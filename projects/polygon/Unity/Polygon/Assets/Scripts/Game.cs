using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public class Vertex
    {
        private static int id = 0;
        public Vector3 location;
        private int _id = Vertex.id++;
        public int ID
        {
            get
            {
                return _id;
            }
        }
    }
    public class VertexVisual
    {
        public GameObject gameObject;
        public Vertex vertex;
        public GameObject selectedGameObject;
    }
    
    public class Edge
    {
        public Vertex A;
        public Vertex B;
    }
    public class EdgeVisual
    {
        public GameObject gameObject;
        public Edge edge;
    }
    
    public class Tri3D
    {
        public Vertex A;
        public Vertex B;
        public Vertex C;

        public bool Contains(Vertex vertex)
        {
            return vertex == A || vertex == B || vertex == C;
        }
    }
    public class Tri3DVisual
    {
        public GameObject gameObject;
        public Tri3D tri;
    }
    
    
    
    
    
    
    
    
    [SerializeField] private OVRCameraRig rig;
    [SerializeField] private GameObject rigTracking;
    [SerializeField] private OVRManager manager;
    
    
    [SerializeField] private GameObject displayControllerLeft;
    [SerializeField] private GameObject displayControllerRight;
    
    
    [SerializeField] private GameObject vertexPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject vertexSelectCoverPrefab;
    
    [SerializeField] private GameObject menuDisplayPrefab;
    
    [SerializeField] private GameObject triangleMesh;

    private MeshFilter triangleMeshFilter;
    
    
    
    private List<VertexVisual> vertexes = new List<VertexVisual>();
    private List<EdgeVisual> edges = new List<EdgeVisual>();
    private List<Tri3DVisual> triangles = new List<Tri3DVisual>();

    // Start is called before the first frame update
    void Start()
    {
        var details = OVRInput.openVRControllerDetails;
        Debug.Log(details);
        var isPrimaryDown = OVRInput.Get(OVRInput.Touch.One);
        Debug.Log($"isPrimaryDown: {isPrimaryDown}");


        XRInterface.Instance.UpdateEventDelegate += ControllerUpdate;
        XRInterface.Instance.dispatch.AddListener(XRInterface.EventNameLeftButtonA,LeftControllerButtonA, null);
        XRInterface.Instance.dispatch.AddListener(XRInterface.EventNameLeftButtonB,LeftControllerButtonB, null);
        
        XRInterface.Instance.dispatch.AddListener(XRInterface.EventNameLongPressLeftButtonA,LeftControllerLongPressButtonA, null);
        XRInterface.Instance.dispatch.AddListener(XRInterface.EventNameLongPressRightButtonA,RightControllerLongPressButtonA, null);
        
        XRInterface.Instance.dispatch.AddListener(XRInterface.EventNameQuickTapRightButtonA,RightControllerQuickTapButtonA, null);
                
        // passthry
        var centerCamera = rig.centerEyeAnchor.GetComponent<Camera>();
        centerCamera.clearFlags = CameraClearFlags.SolidColor;
        centerCamera.backgroundColor = Color.clear;


        triangleMeshFilter = triangleMesh.GetComponent<MeshFilter>();
        
        GenerateTriangleMeshFromGraph();
        
    }

    // Update is called once per frame
    void Update()
    {
        //OVRInput.Controller.RTouch
        
        // OVRInput.Controller.LHand;

        //OVRInput.controller
        var left = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.LeftHanded);
        var right = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.RightHanded);
        var leftPosition = OVRInput.GetLocalControllerPosition(left);
        var rightPosition = OVRInput.GetLocalControllerPosition(right);
        var leftRotation = OVRInput.GetLocalControllerRotation(left);
        var rightRotation = OVRInput.GetLocalControllerRotation(right);
        
        // OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        // OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        // var rigPosition = rig.transform.position;
        
        var rigPosition = rigTracking.transform.position;
        leftPosition += rigPosition;
        rightPosition += rigPosition;
        

        displayControllerLeft.transform.position = leftPosition;
        displayControllerRight.transform.position = rightPosition;
        
        displayControllerLeft.transform.rotation = leftRotation;
        displayControllerRight.transform.rotation = rightRotation;
        
        /*
         // capacitance - any touch of the button at all is a true
        var isLeftADown = OVRInput.Get(OVRInput.Touch.One, left);
        var isRightADown = OVRInput.Get(OVRInput.Touch.One, right);
        var isLeftBDown = OVRInput.Get(OVRInput.Touch.Two, left);
        var isRightBDown = OVRInput.Get(OVRInput.Touch.Two, right);
        */
        /*
         // these both map to right hand?
        var isLeftADown = OVRInput.Get(OVRInput.RawTouch.A, left);
        var isRightADown = OVRInput.Get(OVRInput.RawTouch.A, right);
        var isLeftBDown = OVRInput.Get(OVRInput.RawTouch.B, left);
        var isRightBDown = OVRInput.Get(OVRInput.RawTouch.B, right);
        */
        //var isSecondaryDown = OVRInput.Get(OVRInput.Touch.Two);
        
        // button up/down behavior
        var isLeftADown = OVRInput.Get(OVRInput.Button.One, left);
        var isRightADown = OVRInput.Get(OVRInput.Button.One, right);
        var isLeftBDown = OVRInput.Get(OVRInput.Button.Two, left);
        var isRightBDown = OVRInput.Get(OVRInput.Button.Two, right);
        
        var wasLeftADown = OVRInput.GetDown(OVRInput.Touch.One, left);
        var wasRightADown = OVRInput.GetDown(OVRInput.Touch.One, right);

        if (wasLeftADown)
        {
//            OVRInput.SetControllerVibration(0.01f, 0.5f, left);
        }
        if (wasRightADown)
        {
//            OVRInput.SetControllerVibration(0.01f, 0.5f, right);
        }

    }

    void ControllerUpdate()
    {
        var currentLocationLeft = XRInterface.Instance.leftLocation();
        var currentRotationLeft = XRInterface.Instance.leftRotation();


        Quaternion menuRotation = Quaternion.AngleAxis(90.0f + 45.0f, Vector3.up);
        float menuVerticalOffset = 0.35f;
        Vector3 leftUp = currentRotationLeft * new Vector3(0.0f, menuVerticalOffset, 0.0f);
        menuDisplayPrefab.transform.rotation = currentRotationLeft * menuRotation;
        menuDisplayPrefab.transform.position = currentLocationLeft + leftUp;
        
        if (SelectedVertexes.Count > 0 && selectLocationStart != null && XRInterface.Instance.isLeftButtonADown())
        {
            Vector3 diff = currentLocationLeft - selectLocationStart.Value;
            selectLocationStart += diff;
            foreach (var vertex in SelectedVertexes)
            {
                vertex.vertex.location += diff;
                vertex.gameObject.transform.position = vertex.vertex.location;
                vertex.selectedGameObject.transform.position = vertex.gameObject.transform.position;
            }

            GenerateTriangleMeshFromGraph(true);
        }

        if (SelectedVertexes.Count == 0 && previousVertex != null && potentialLine != null)
        {
            var line = currentLocationLeft - previousVertex.vertex.location;
            potentialLine.transform.position = previousVertex.vertex.location;
            potentialLine.transform.rotation = Quaternion.LookRotation(line);
            potentialLine.transform.localScale = new Vector3(1.0f,1.0f,line.magnitude);
        }
    }
    
    void LeftControllerButtonA(System.Object value, System.Object context)
    {
        Debug.Log($"LeftControllerButtonA: {value}");

        if (value.Equals(true))
        {
            var position = XRInterface.Instance.leftLocation();
            
            var closest = ClosestVertexToPosition(position);
            if (closest == null || Vector3.Distance(closest.vertex.location, position) > limitVertexDistanceNeighbor)
            {
                if (SelectedVertexes.Count > 0)
                {
                    selectLocationStart = XRInterface.Instance.leftLocation();
                    // selectLocationStart = null;
                }
                else
                {
                    previousVertex = AddVertexAt(position);
                    if (potentialLine == null)
                    {
                        potentialLine = Instantiate(linePrefab);
                        potentialLine.SetActive(true);
                    }

                }
            }
            else // closest
            {
                if (SelectedVertexes.Contains(closest))
                {
                    // deselect single vertex
                    SelectedVertexes.Remove(closest);
                    Destroy(closest.selectedGameObject);
                    closest.selectedGameObject = null;
                    selectLocationStart = null;
                }
                else
                {
                    // select single vertex
                    // selectLocationStart = XRInterface.Instance.leftLocation();
                    selectLocationStart = null;
                    var cover = Instantiate(vertexSelectCoverPrefab);
                    cover.SetActive(true);
                    cover.transform.position = closest.vertex.location;
                    closest.selectedGameObject = cover;
                    SelectedVertexes.Remove(closest); // make sure only 1 at a time // TODO: this shouldn't happen now
                    SelectedVertexes.Add(closest);
                }
            }
        

        }
        else
        {
            if (SelectedVertexes.Count > 0 && selectLocationStart != null)
            {
                //var currentLocation = XRInterface.Instance.leftLocation();
                //Vector3 diff = currentLocation - selectLocationStart.Value;
                foreach (var vertex in SelectedVertexes)
                {
                    // vertex.vertex.location += diff;
                    vertex.gameObject.transform.position = vertex.vertex.location;
                    vertex.selectedGameObject.transform.position = vertex.vertex.location;
                }
                // GenerateTriangleMeshFromGraph(true);
            }
        }
    }


    
    void LeftControllerButtonB(System.Object value, System.Object context)
    {
        if (value.Equals(true))
        {
            if (SelectedVertexes.Count > 0)
            {
                var position = XRInterface.Instance.leftLocation();
                var geometryWasChanged = false;
                // remove display no matter what
                foreach (var vertex in SelectedVertexes)
                {
                    vertex.gameObject.transform.position = vertex.vertex.location;
                    vertex.selectedGameObject.SetActive(false);
                    Destroy(vertex.selectedGameObject);
                    vertex.selectedGameObject = null;
                }
                
                // delete if a selected vertex is covered only
                var closest = ClosestVertexToPosition(position);
                if (closest != null && Vector3.Distance(closest.vertex.location, position) < limitVertexDistanceNeighbor)
                {
                    // delete vertexes if selecting B on one
                    if (SelectedVertexes.Contains(closest))
                    {
                        foreach (var vertex in SelectedVertexes)
                        {
                            Destroy(vertex.gameObject);
                            vertexes.Remove(vertex);
                            // remove connected faces
                            foreach (var tri in triangles)
                            {
                                if (tri.tri.Contains(vertex.vertex))
                                {
                                    triangles.Remove(tri);
                                }
                            }
                        }
                    }
                    geometryWasChanged = true;
                }
                
                GenerateTriangleMeshFromGraph(geometryWasChanged);
                
                previousVertex = null;
                SelectedVertexes.Clear();
            }
        }
    }

    void LeftControllerLongPressButtonA(System.Object value, System.Object context)
    {
        Debug.Log("LeftControllerLongPressButtonA");
    }

    void RightControllerLongPressButtonA(System.Object value, System.Object context)
    {
        Debug.Log("RightControllerLongPressButtonA");
    }
    
    void RightControllerQuickTapButtonA(System.Object value, System.Object context)
    {
        bool wasTapCount2 = value.Equals(2);
        if (!wasTapCount2)
        {
            return;
        }
        
        Debug.Log("RightControllerLongPressButtonA");
        if (SelectedVertexes.Count == 3)
        {
            var a = SelectedVertexes[0];
            var b = SelectedVertexes[1];
            var c = SelectedVertexes[2];
            var tri = new Tri3D
            {
                A = a.vertex,
                B = b.vertex,
                C = c.vertex
            };
            if ( SimilarTriExists(tri) == null)
            {
                var visual = new Tri3DVisual
                {
                    gameObject = null,
                    tri = tri
                };
                triangles.Add(visual);
                GenerateTriangleMeshFromGraph(); // REACHED
            }
        }
        
//        GenerateTriangleMeshFromGraph(); // REACHED
    }

    private Tri3D SimilarTriExists(Tri3D tri)
    {
        foreach(var visual in triangles)
        {
            var oth = visual.tri;
            if (oth.Contains(tri.A) && oth.Contains(oth.B) && oth.Contains(tri.C))
            {
                return oth;
            }
        }

        return null;
    }

    private List<VertexVisual> SelectedVertexes = new List<VertexVisual>();

    private VertexVisual AddVertexAt(Vector3 position)
    {
        var go = Instantiate(vertexPrefab);
        go.transform.position = position;
        go.SetActive(true);
        var vertex = new Vertex();
        vertex.location = position;
        var vv = new VertexVisual();
        vv.gameObject = go;
        vv.vertex = vertex;
        vertexes.Add(vv);
        return vv;
    }

    private VertexVisual ClosestVertexToPosition(Vector3 location)
    {
        VertexVisual closest = null;
        var distance = 0.0f;
        foreach (var vv in vertexes)
        {
            var v = vv.vertex;
            var d = Vector3.Distance(v.location, location);
            if (closest == null || d < distance)
            {
                distance = d;
                closest = vv;
            }
        }

        return closest;
    }
    private float limitVertexDistanceNeighbor = 0.025f; // no closer than 2.5cm
    //private List<GameObject> vertexSelectCovers = new List<GameObject>();
    private Vector3? selectLocationStart = null;
    private VertexVisual previousVertex;
    
    private GameObject potentialLine;


//    private float off = -0.50f; 
    private void GenerateTriangleMeshFromGraph(bool updateOnly = false)
    {
//        off += 0.1f;
        MeshFilter meshFilter = triangleMeshFilter;
        Mesh mesh;

        Vector3[] newVertices;
        int[] newTriangles;
        // Vector2[] newUV;
        if (updateOnly) // use existing
        {
            mesh = meshFilter.mesh;
            newVertices = mesh.vertices;
            // newUV = mesh.uv;
            newTriangles = mesh.triangles;
        }
        else // create new
        {
            mesh = new Mesh();
            meshFilter.mesh = mesh;
            newVertices = new Vector3[vertexes.Count];
            newTriangles = new int[triangles.Count*3*2];
        }

        var vertexIDtoIndex = new Dictionary<int,int>();
        int i = 0;
        foreach(var vv in vertexes)
        {
            vertexIDtoIndex[vv.vertex.ID] = i;
            newVertices[i] = vv.vertex.location;
            i += 1;
        }
            
        i = 0;
        foreach (var vt in triangles)
        {
            int a = vertexIDtoIndex[vt.tri.A.ID];
            int b = vertexIDtoIndex[vt.tri.B.ID];
            int c = vertexIDtoIndex[vt.tri.C.ID];
            // newTriangles[i*3 + 0] = a;
            // newTriangles[i*3 + 1] = b;
            // newTriangles[i*3 + 2] = c;
            newTriangles[i*6 + 0] = a;
            newTriangles[i*6 + 1] = b;
            newTriangles[i*6 + 2] = c;
            newTriangles[i*6 + 3] = c;
            newTriangles[i*6 + 4] = b;
            newTriangles[i*6 + 5] = a;
            i += 1;
        }
        
        mesh.vertices = newVertices;
        // mesh.uv = newUV;
        mesh.triangles = newTriangles;
/*
        // TEST:
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        Vector3[] newVertices2 = {
            new Vector3 (0+off, 0, 0),
            new Vector3 (1+off, 0, 0),
            new Vector3 (0+off, 1, 0),
            new Vector3 (0+off, 0, 1),
        };
        int[] newTriangles2 =
        {
            0,2,1,
            0,1,3,
            0,3,2,
            1,2,3
        };
        mesh.vertices = newVertices2;
        mesh.triangles = newTriangles2;
*/
    }
    
    /*
     start w/ an existing vertex
     on press:
        - close to an existing vertex ?
            - join
        - else:
            - create an edge if prevVertex exists
        
     LIMIT FOR JOINING:
        - if shortest distance between new vertexes is > 3 => NO
        - if shortest distance is 1/2 => NO (only 3-faces is valid)
        -  
     */
}
