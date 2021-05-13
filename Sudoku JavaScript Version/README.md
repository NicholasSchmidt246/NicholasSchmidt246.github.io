This Directory is a clone of:

https://github.com/dachev/sudoku 

as a starting point for my CS-499-T5477 Computer Science Capstone 21EW5 course at Southern New Hampshire University

The purpose of this course is to demonstrate various skillsets I have developed in my time as a student.


# sudoku
      

Sudoku generator and solver for [node.js](http://nodejs.org) and Web.

## Live demo
[http://blago.dachev.com](http://blago.dachev.com)

## Installation

``` bash
    $ npm install sudoku
```

## Usage

``` javascript
    var sudoku = require('sudoku');

    var puzzle     = sudoku.makepuzzle();
    var solution   = sudoku.solvepuzzle(puzzle);
    var difficulty = sudoku.ratepuzzle(puzzle, 4);
```
OR

``` typescript
    import { makepuzzle, solvepuzzle, ratepuzzle } from "sudoku";
```


## License
Copyright 2010, Blagovest Dachev.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
