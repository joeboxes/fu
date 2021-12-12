# crossword puzzle project


## planning

### wireframes - screens

- app landing page
	-> settings
	-> puzzles
	-> 

- settings
	- sound
	- color/themes
	- language

- user info
	- puzzles completed
	- 

- puzzle groups
	- sorting [fwd / rev]
		- title a-z
		- difficulty [grid size ?]
		
- puzzle list

- puzzle
	- timer
	- image
	- grid
	- questions
	- 
	- empty
	- progress
	- complete



### data 

*puzzle data*
```
result: success
payload:
	puzzle:
		id: 1234567890ABCDEF
		title: Dinosaurs
		iconURL: http://www.google.com/image.jpg
		featureURL: http://www.google.com/image.jpg
		????URL: http://www.google.com/image.jpg
		language: en
			width: 10
			height: 10
			grid: "asqriclworciwclvoeriwsd..."
			hints:
				- 
					display: Eats branches
					ends:
						- 
							x: 0
							y: 1
						- 
							x: 9
							y: 1
			time: 420 # seconds
			dificulty: 4 # 0-9  easy 0-2  med 3-6  hard 7-9

```

*puzzle list*
```
result: success
payload:
	offset: 10
	count: 10
	puzzles: # []
		- 
			id: 1234567890ABCDEF
			title: Dinosaurs
			description: All things dino saurus
			iconURL: http://www.google.com/image.jpg
			width: 8
			height: 8
		- ...

```

*puzzle groups/categories*
```
result: success
payload:
	offset: 10
	count: 10
	groups: # []
		- 
			id: 1234567890ABCDEF
			title: Animals
			description: Everyone likes animals don't they?
			iconURL: http://www.google.com/image.jpg
			count: 12
		- ...

```

- puzzle groupings
	- science
	- math
	- electronics
	- astronomy
	- technology
	- animals
	- evolution
	- dinosaurs
	- ...
	- easy
	- medium
	- hard
	- ...
	- all

- puzzle list
	-> given grouping
	-> return list of puzzles
- puzzle details





## development



### steps
- landing screen buttons
- navigating between screens
- draw shapes onto screen
- draw a crossword box onto screen
- scrollview with list
- read from file
- read data into local DTOs
- question/answer list [mini version & expanded version]
- tracking user point/drag/release events
	https://www.youtube.com/watch?v=ERAN5KBy2Gs
- draw pills around screen
	https://www.youtube.com/watch?v=--LB7URk60A
- 

##############

- event system to soft connect unity events & other

- 
..




## release



# references


...

...