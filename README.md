A squad of robotic rovers are to be landed by NASA on a plateau on Mars. The navigation team needs a
utility for them to simulate rover movements so they can develop a navigation plan.
A rover's position is represented by a combination of an x and y co-ordinates and a letter
representing one of the four cardinal compass points. The plateau is divided up into a grid to
simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
left corner and facing North.
In order to control a rover, NASA sends a simple string of letters. The possible letters are:
- 'L'. Make the rover spin 90 degrees left without moving from its current spot.
- 'R'. Make the rover spin 90 degrees right without moving from its current spot.
- 'M'. Move forward one grid point, and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).


INPUT
The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are
assumed to be 0,0.
The rest of the input is information pertaining to the rovers that have been deployed. Each rover
has two lines of input. The first line gives the rover's position, and the second line is a series of
instructions telling the rover how to explore the plateau.
The position is made up of two integers and a letter separated by spaces, corresponding to the x
and y co-ordinates and the rover's orientation.
Each rover will be finished sequentially, which means that the second rover won't start to move
until the first one has finished moving.

OUTPUT
The output for each rover should be its final co-ordinates and heading.
Example Program Flow:
Enter Graph Upper Right Coordinate: 5 5
Rover 1 Starting Position: 1 2 N
Rover 1 Movement Plan: LMLMLMLMM
Rover 1 Output: 1 3 N
Rover 2 Starting Position: 3 3 E
Rover 2 Movement Plan: MMRMMRMRRM
Rover 2 Output: 5 1 E

Assumptions:

The rover can only move within the given area.
If the rover is given a command to move outside of the area, no action will be taken.

Only one rover can occopy a given space at a time.
If one rover is given a command to move into a space that another rover currently occupies, no action will be taken.

Notes:
I try to approach most projects by trying to get a simple working prototype quickly.
In this case, I created the KISS(Keep It Simple Silly)Project. It took about an hour to complete and had all of the basic components.

I enjoyed thinking about the problem and looking into extending it a bit. These ruminations caused me to create the EnterpriseProject.
I used more generics thinking that while coordinates were currently given in integers, a logic real world application would most likely use decimals.
The basic app assumes a rectangle. The Enterprise application laid some of the groundwork for having different shaped polygons.