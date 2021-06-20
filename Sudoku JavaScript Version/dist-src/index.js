// Sudoku Generator and Solver for node.js
// Copyright (c) 2011 Blagovest Dachev.  All rights reserved.
//
// This is a port of David Bau's python  implementation:
// http://davidbau.com/archives/2006/09/04/sudoku_generator.html

// Description made by NS
/* 
 * makepuzzle
 * input:           board: any
 * dependencies:    shuffleArray, deduce, removeElement, boardforentries, checkpuzzle
 * output:          any[] 
 * notes:           side effects
 */
function makepuzzle(board) {
  var puzzle = [];
  var deduced = Array(81).fill(null);
  var order = [...Array(81).keys()];
  shuffleArray(order); // shuffleArray(original: any): void - NS

  for (var i = 0; i < order.length; i++) {
    var pos = order[i];

    if (deduced[pos] === null) {
      puzzle.push({
        pos: pos,
        num: board[pos]
      });
      deduced[pos] = board[pos];
      deduce(deduced); // deduce(board: any): any - This appears to be used exclusively for it's side effects, this has an unused return type - NS
    }
  }

  shuffleArray(puzzle); // shuffleArray(original: any): void - NS

  for (var i = puzzle.length - 1; i >= 0; i--) {
    var e = puzzle[i];
    removeElement(puzzle, i); // Another example of side effects - NS
    var rating = checkpuzzle(boardforentries(puzzle), board); // boardforentries(entries: any):any[] && checkpuzzle(puzzle:any, board:any): any

    if (rating === -1) {
      puzzle.push(e);
    }
  }

  return boardforentries(puzzle);
}

// Description made by NS
/*
 * {Method Name}
 * input:           puzzle: any, samples: any
 * dependencies:    solveboard
 * output:          number
 * notes:           
 */
function ratepuzzle(puzzle, samples) {
  var total = 0;

  for (var i = 0; i < samples; i++) {
    var tuple = solveboard(puzzle); // solveboard(original: any): { state: any, answer: any[] } - NS

    if (tuple.answer === null) {
      return -1;
    }

    total += tuple.state.length;
  }

  return total / samples;
}

// Description made by NS
/*
 * checkpuzzle
 * input:           puzzle: any, board: any
 * dependencies:    solveboard, boardmatches, solvenext
 * output:          any
 * notes:           
 */
function checkpuzzle(puzzle, board) {
  if (board === undefined) {
    board = null;
  }

  var tuple1 = solveboard(puzzle); // solveboard(board: any): { state: any, answer: any[] } - NS

  if (tuple1.answer === null) {
    return -1;
  }

    if (board != null && !boardmatches(board, tuple1.answer)) { // boardmatches(b1: any, b2: any): boolean - NS
    return -1;
  }

  var difficulty = tuple1.state.length;
  var tuple2 = solvenext(tuple1.state); // solvenext(remembered: any): { state: any, answer: any[] } - NS

  if (tuple2.answer != null) {
    return -1;
  }

  return difficulty;
}

// Description made by NS
/*
 * solvepuzzle
 * input:           board: any
 * dependencies:    solveboard
 * output:          any[]
 * notes:           This function is unnessary, it's call can be replaced by the one line it contains
 */
function solvepuzzle(board) {
  return solveboard(board).answer; // solveboard(board: any): { state: any, answer: any[] } - NS
}

// Description made by NS
/*
 * solveboard
 * input:           original: any
 * dependencies:    deduce, solvenext
 * output:          { state: any, answer: any[] }
 * notes:           
 */
function solveboard(original) {
  var board = [].concat(original);
  var guesses = deduce(board); // deduce(board: any): any - NS

  if (guesses === null) {
    return {
      state: [],
      answer: board
    };
  }

  var track = [{
    guesses: guesses,
    count: 0,
    board: board
  }];
  return solvenext(track); // solvenext(remembered: any): { state: any, answer: any[] } - NS
}

// Description made by NS
/*
 * solvenext
 * input:           remembered: any
 * dependencies:    deduce
 * output:          { state: any, answer: any[] }
 * notes:           side effects
 */
function solvenext(remembered) {
  while (remembered.length > 0) {
    var tuple1 = remembered.pop();

    if (tuple1.count >= tuple1.guesses.length) {
      continue;
    }

    remembered.push({
      guesses: tuple1.guesses,
      count: tuple1.count + 1,
      board: tuple1.board
    });
    var workspace = [].concat(tuple1.board);
    var tuple2 = tuple1.guesses[tuple1.count];
    workspace[tuple2.pos] = tuple2.num;
    var guesses = deduce(workspace); // deduce(board: any): any - NS

    if (guesses === null) {
      return {
        state: remembered,
        answer: workspace
      };
    }

    remembered.push({
      guesses: guesses,
      count: 0,
      board: workspace
    });
  }

  return {
    state: [],
    answer: null
  };
}

// Description made by NS
/*
 * deduce
 * input:           board: any
 * dependencies:    figurebits, listbits, pickbetter, posfor, shufflearray
 * output:          any 
 * notes:           viewable responses include the following: [], { pos: number, val: number }, and any) 
 *                  side effects
 *                  magic numbers
 */
function deduce(board) {
  while (true) {
    var stuck = true;
    var guess = null;
    var count = 0;

    var tuple1 = figurebits(board); // figurebits(board: any): { allowed: any, needed: number[] } - NS
    var allowed = tuple1.allowed; // This variable shares a name with a the function - allowed(board: any, position: any) : number - NS
    var needed = tuple1.needed;

    for (var pos = 0; pos < 81; pos++) {
      if (board[pos] === null) {
        var numbers = listbits(allowed[pos]); // listbits(bits: any): number[] - NS

        if (numbers.length === 0) {
          return [];
        } else if (numbers.length === 1) {
          board[pos] = numbers[0];
          stuck = false;
        } else if (stuck) {
            var t = numbers.map(function (val, key) { // This appears to be defining a method for each element, requiring element key, which appears unused, and return pos which it gets from where? - NS
            return {
              pos: pos,
              num: val
            };
          });
          var tuple2 = pickbetter(guess, count, t); // pickbetter(b: any, c: any, t: any): { guess: any, count: any } - NS
          guess = tuple2.guess;
          count = tuple2.count;
        }
      }
    }

    if (!stuck) {
      var tuple3 = figurebits(board); // figurebits(board: any): { allowed: any, needed: number[] } - NS
      allowed = tuple3.allowed;
      needed = tuple3.needed;
    }


    for (var axis = 0; axis < 3; axis++) {
      for (var x = 0; x < 9; x++) {
        var numbers = listbits(needed[axis * 9 + x]); // listbits(bits: any): number[] - NS

        for (var i = 0; i < numbers.length; i++) {
          var n = numbers[i];
          var bit = 1 << n;
          var spots = [];

          for (var y = 0; y < 9; y++) {
            var pos = posfor(x, y, axis); // posfor(x: any, y: any, axis: any): any - NS

            if (allowed[pos] & bit) {
              spots.push(pos);
            }
          }

          if (spots.length === 0) {
            return [];
          } else if (spots.length === 1) {
            board[spots[0]] = n;
            stuck = false;
          } else if (stuck) {
              var t = spots.map(function (val, key) { // another mapping - NS
              return {
                pos: val,
                num: n
              };
            });
            var tuple4 = pickbetter(guess, count, t); // pickbetter(b: any, c: any, t: any): { guess: any, count: any } - NS
            guess = tuple4.guess;
            count = tuple4.count;
          }
        }
      }
    }

    if (stuck) {
      if (guess != null) {
        shuffleArray(guess); // shuffleArray(original: any): void - NS
      }

      return guess;
    }
  }
}

// Description made by NS
/*
 * figurebits
 * input:           board: any
 * dependencies:    axismissing, posfor
 * output:          { allowed: any, needed: number[] }
 * notes:           magic numbers, bitwise operations
 */
function figurebits(board) {
  var needed = [];
  var allowed = board.map(function (val, key) { // mapping a function to check for null and return either 511 or 0) why is very unclear also appears to be mapping to an empty array? - NS
    return val === null ? 511 : 0;
  }, []);

  for (var axis = 0; axis < 3; axis++) {
    for (var x = 0; x < 9; x++) {
      var bits = axismissing(board, x, axis); // axismissing(board: any, x: any, axis: any): number - NS
      needed.push(bits);

      for (var y = 0; y < 9; y++) {
        var pos = posfor(x, y, axis); // posfor(x: any, y: any, axis: any): any - NS
        allowed[pos] = allowed[pos] & bits; // Why are we executing bitwise operations? - NS
      }
    }
  }

  return {
    allowed: allowed,
    needed: needed
  };
}

// Description made by NS
/*
 * posfor
 * input:           x: any, y: any, axis: any
 * dependencies:    none
 * output:          any
 * notes:           magic numbers
 */
function posfor(x, y, axis) {
  if (axis === undefined) {
    axis = 0;
  }

  if (axis === 0) {
    return x * 9 + y;
  } else if (axis === 1) {
    return y * 9 + x;
  }

  return [0, 3, 6, 27, 30, 33, 54, 57, 60][x] + [0, 1, 2, 9, 10, 11, 18, 19, 20][y]; // What is the purpose for these numbers? - NS
}

// Description made by NS
/*
 * axisfor
 * input:           pos: any, axis: any
 * dependencies:    Math library
 * output:          number
 * notes:           magic numbers
 */
function axisfor(pos, axis) {
  if (axis === 0) {
    return Math.floor(pos / 9);
  } else if (axis === 1) {
    return pos % 9;
  }

  return Math.floor(pos / 27) * 3 + Math.floor(pos / 3) % 3;
}

// Description made by NS
/*
 * {Method Name}
 * input:           name: type
 * dependencies:    posfor
 * output:          type
 * notes:           magic numbers
 *                  bitwise opperations
 */
function axismissing(board, x, axis) {
  var bits = 0;

  for (var y = 0; y < 9; y++) {
    var e = board[posfor(x, y, axis)];

    if (e != null) {
      bits |= 1 << e;
    }
  }

  return 511 ^ bits;
}

// Description made by NS
/*
 * listbits
 * input:           bits: any
 * dependencies:    none
 * output:          number[]
 * notes:           bitwise opperations
 */
function listbits(bits) {
  var list = [];

  for (var y = 0; y < 9; y++) {
    if ((bits & 1 << y) != 0) { // Why are we executing bitwise opperations? - NS
      list.push(y);
    }
  }

  return list;
}

// Description made by NS
/*
 * allowed
 * input:           board: any, pos: any
 * dependencies:    axisfor, axismissing
 * output:          number
 * notes:           does not appear to be called by anything, the TODO: seems to imply this might not be complete.
 *                  magic numbers
 *                  bitwise opperations
 */
function allowed(board, pos) {
  var bits = 511;

  for (var axis = 0; axis < 3; axis++) {
    var x = axisfor(pos, axis);
    bits = bits & axismissing(board, x, axis);
  }

  return bits;
} // TODO: make sure callers utilize the return value correctly

// Description made by NS
/*
 * pickbetter
 * input:           b: any, c: any, t: any
 * dependencies:    randomInt
 * output:          { guess: any, count: any }
 * notes:           illegible
 */
function pickbetter(b, c, t) {
  if (b === null || t.length < b.length) {
    return {
      guess: t,
      count: 1
    };
  } else if (t.length > b.length) {
    return {
      guess: b,
      count: c
    };
  } else if (randomInt(c) === 0) { // randomInt(max: any): number - NS
    return {
      guess: t,
      count: c + 1
    };
  }

  return {
    guess: b,
    count: c + 1
  };
}

// Description made by NS
/*
 * boardforentries
 * input:           entries: any
 * dependencies:    none
 * output:          any[]
 * notes:           magic numbers
 */
function boardforentries(entries) {
  var board = Array(81).fill(null);

  for (var i = 0; i < entries.length; i++) {
    var item = entries[i];
    var pos = item.pos;
    var num = item.num;
    board[pos] = num;
  }

  return board;
}

// Description made by NS
/*
 * boardmatches
 * input:           b1: any, b2: any
 * dependencies:    none
 * output:          boolean
 * notes:           magic numbers
 */
function boardmatches(b1, b2) {
  for (var i = 0; i < 81; i++) {
    if (b1[i] != b2[i]) {
      return false;
    }
  }

  return true;
}

// Description made by NS
/*
 * randomInt
 * input:           max: any
 * dependencies:    Math library
 * output:          number
 * notes:           this is a needless function, the call could easily be replaced with the one line it contains
 */
function randomInt(max) {
  return Math.floor(Math.random() * (max + 1));
}

// Description made by NS
/* 
 * shuffleArray
 * input:           original: any
 * dependencies:    randomInt
 * output:          void
 * notes:           side effects
 */
function shuffleArray(original) {
  for (var i = original.length - 1; i > 0; i--) {
    var j = randomInt(i); // randomInt(max: any): number - NS
    var contents = original[i];
    original[i] = original[j];
    original[j] = contents;
  }
}

// Description made by NS
/*
 * removeElement
 * input:           array: any, from: any, to: any
 * dependencies:    none
 * output:          any
 * notes:
 */
function removeElement(array, from, to) {
  var rest = array.slice((to || from) + 1 || array.length);
  array.length = from < 0 ? array.length + from : from;
  return array.push.apply(array, rest);
}

;
module.exports = {
  makepuzzle: function () {
    return makepuzzle(solvepuzzle(Array(81).fill(null)));
  },
  solvepuzzle: solvepuzzle,
  ratepuzzle: ratepuzzle,
  posfor: posfor
};