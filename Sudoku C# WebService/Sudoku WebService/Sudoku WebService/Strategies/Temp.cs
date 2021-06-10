using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Strategy
{
    public class SudokuPuzzle
    {
        #region In Progress

        #region AxisMissing

        public void AxisMissing(object board, object x, object axis)
        {

        }
        //function axismissing(board, x, axis)
        //{
        //    var bits = 0;

        //    for (var y = 0; y < 9; y++)
        //    {
        //        var e = board[posfor(x, y, axis)];

        //        if (e != null)
        //        {
        //            bits |= 1 << e;
        //        }
        //    }

        //    return 511 ^ bits;
        //}

        #endregion
        #region Posfor

        public void Posfor(object x, object y, object axis)
        {

        }
        //function posfor(x, y, axis)
        //{
        //    if (axis === undefined)
        //    {
        //        axis = 0;
        //    }

        //    if (axis === 0)
        //    {
        //        return x * 9 + y;
        //    }
        //    else if (axis === 1)
        //    {
        //        return y * 9 + x;
        //    }

        //    return [0, 3, 6, 27, 30, 33, 54, 57, 60][x] + [0, 1, 2, 9, 10, 11, 18, 19, 20][y];
        //}

        #endregion
        #region FigureBits
        public Idk FigureBits(object board)
        {
            return null;
        }
        //    function figurebits(board)
        //    {
        //        var needed = [];
        //        var allowed = board.map(function(val, key) {
        //            return val === null ? 511 : 0;
        //        }, []);

        //        for (var axis = 0; axis < 3; axis++)
        //        {
        //            for (var x = 0; x < 9; x++)
        //            {
        //                var bits = axismissing(board, x, axis);
        //                needed.push(bits);

        //                for (var y = 0; y < 9; y++)
        //                {
        //                    var pos = posfor(x, y, axis);
        //                    allowed[pos] = allowed[pos] & bits;
        //                }
        //            }
        //        }

        //        return {
        //            allowed: allowed,
        //needed: needed
        //        };
        //    }
        #endregion
        #region ListBits
        public object ListBits(object bits)
        {
            return null;
        }
        //function listbits(bits)
        //{
        //    var list = [];

        //    for (var y = 0; y < 9; y++)
        //    {
        //        if ((bits & 1 << y) != 0)
        //        {
        //            list.push(y);
        //        }
        //    }

        //    return list;
        //}
        #endregion
        #region PickBetter
        public void PickBetter(object b, object c, object t)
        {

        }

    //    function pickbetter(b, c, t)
    //    {
    //        if (b === null || t.length < b.length)
    //        {
    //            return {
    //                guess: t,
    //  count: 1
    //            };
    //        }
    //        else if (t.length > b.length)
    //        {
    //            return {
    //                guess: b,
    //  count: c
    //            };
    //        }
    //        else if (randomInt(c) === 0)
    //        {
    //            return {
    //                guess: t,
    //  count: c + 1
    //            };
    //        }

    //        return {
    //            guess: b,
    //count: c + 1
    //        };
    //    }

        #endregion
        #region Deduce
        public List<Entry> Deduce(List<int> board)
        {
            throw new NotImplementedException();
            //while (true)
            //{
            //    var stuck = true;
            //    var guess = null;
            //    var count = 0; // fill in any spots determined by direct conflicts

            //    Idk tuple1 = FigureBits(board);
            //    var allowed = tuple1.Allowed;
            //    var needed = tuple1.Needed;

            //    for (var pos = 0; pos < board.Count; pos++)
            //    {
            //        if (board[pos] == null)
            //        {
            //            var numbers = ListBits(allowed[pos]);

            //            if (number.length == 0)
            //            {
            //                return [];
            //            }
            //            else if (numbers.length == 1)
            //            {
            //                board[pos] = numbers[0];
            //                stuck = false;
            //            }
            //        }
            //    }
            //}

            //                
            //                    
            //                    else if (stuck)
            //                    {
            //                        var t = numbers.map(function(val, key)
            //                                {
            //                            return new object()
            //                                    {
            //                                        pos: pos,
            //                                        num: val
            //                                    };
            //                        });
            //            var tuple2 = PickBetter(guess, count, t);
            //            guess = tuple2.guess;
            //            count = tuple2.count;
            //        }
            //    }
            //}
            //                    if(!stuck)
            //                        var tuple3 = FigureBits(board);
        }
        //public void Deduce(List<int> board)
        //{
        //    while (true)
        //    {
        //        var stuck = true;
        //        var guess = null;
        //        var count = 0; // fill in any spots determined by direct conflicts

        //        var tuple1 = figurebits(board);
        //        var allowed = tuple1.allowed;
        //        var needed = tuple1.needed;

        //        for (var pos = 0; pos < board.Count; pos++)
        //        {
        //            if (board[pos] == null)
        //            {
        //                var numbers = listbits(allowed[pos]);

        //                if (number.length == 0)
        //                {
        //                    return [];
        //                }
        //                else if (numbers.length == 1)
        //                {
        //                    board[pos] = numbers[0];
        //                    stuck = false;
        //                }
        //                else if (stuck)
        //                {
        //                    var t = numbers.map(function(val, key)
        //                    {
        //                        return new object()
        //                        {
        //                            pos: pos,
        //                            num: val
        //                        };
        //                    });
        //                    var tuple2 = pickbetter(guess, count, t);
        //                    guess = tuple2.guess;
        //                    count = tuple2.count;
        //                }
        //            }
        //        }
        //        if(!stuck)
        //            var tuple3 = figurebits(board);
        //    }
        //}

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------

        //    allowed = tuple3.allowed;
        //      needed = tuple3.needed;
        //    } // fill in any spots determined by elimination of other locations


        //for (var axis = 0; axis < 3; axis++)
        //{
        //    for (var x = 0; x < 9; x++)
        //    {
        //        var numbers = listbits(needed[axis * 9 + x]);

        //        for (var i = 0; i < numbers.length; i++)
        //        {
        //            var n = numbers[i];
        //            var bit = 1 << n;
        //            var spots = [];

        //            for (var y = 0; y < 9; y++)
        //            {
        //                var pos = posfor(x, y, axis);

        //                if (allowed[pos] & bit)
        //                {
        //                    spots.push(pos);
        //                }
        //            }

        //            if (spots.length === 0)
        //            {
        //                return [];
        //            }
        //            else if (spots.length === 1)
        //            {
        //                board[spots[0]] = n;
        //                stuck = false;
        //            }
        //            else if (stuck)
        //            {
        //                var t = spots.map(function(val, key) {
        //                    return {
        //                        pos: val,
        //                num: n
        //                    };
        //                });
        //var tuple4 = pickbetter(guess, count, t);
        //guess = tuple4.guess;
        //count = tuple4.count;
        //          }
        //        }
        //      }
        //    }

        //    if (stuck)
        //{
        //    if (guess != null)
        //    {
        //        shuffleArray(guess);
        //    }

        //    return guess;
        //}
        //  }
        //}

        #endregion

        #endregion
        #region Migrated

        public List<int> MakePuzzle(List<int> board)
        {
            // what is the purpose for EACH of the boards?
            var puzzle = new List<Entry>();                         // This is likely the list of entries from any source
            var deduced = InitializeIntList(board.Count);
            var order = InitializeKeyedIntList(board.Count);                 // This list is meant to be an ordered list

            order = Shuffle(order);                                 // Shuffle the ordered list

            for (var index = 0; index < order.Count; index++)
            {
                int pos = order[index];

                if (deduced[index] == 0)
                {
                    puzzle.Add(new Entry { Position = pos, Value = board[pos] });
                    deduced[index] = board[index];
                    Deduce(deduced);
                }
            }

            Shuffle(puzzle);

            for (var index = puzzle.Count - 1; index >= 0; index--)
            {
                var e = puzzle[index];
                RemoveElement(ref puzzle, index, index);
                var rating = CheckPuzzle(BoardForEntries(puzzle, board.Count), board);

                if (rating == -1)
                {
                    puzzle.Add(e);
                }
            }

            return BoardForEntries(puzzle, board.Count);
        }
        public Tuple1 SolveBoard(List<int> original)
        {
            var board = original;
            var guesses = Deduce(board);

            if (guesses == null)
            {
                return new Tuple1()
                {
                    state = new Queue<Tuple2>(),
                    answer = board
                };
            }

            var track = new Queue<Tuple2>();
            track.Enqueue(new Tuple2 { guesses = guesses, count = 0, board = board });
            return SolveNext(track);
        }
        public Tuple1 SolveNext(Queue<Tuple2> remembered)
        {
            while (remembered.Count > 0)
            {
                var tuple1 = remembered.Dequeue();

                if (tuple1.count >= tuple1.guesses.Count)
                {
                    continue;
                }

                remembered.Enqueue(new Tuple2
                {
                    guesses = tuple1.guesses,
                    count = tuple1.count + 1,
                    board = tuple1.board
                });
                var workspace = tuple1.board;
                var tuple2 = tuple1.guesses[tuple1.count];
                workspace[tuple2.Position] = tuple2.Value;
                var guesses = Deduce(workspace);

                if (guesses == null)
                {
                    return new Tuple1()
                    {
                        state = remembered,
                        answer = workspace
                    };
                }

                remembered.Enqueue(new Tuple2()
                {
                    guesses = guesses,
                    count = 0,
                    board = workspace
                });
            }

            return new Tuple1
            {
                state = new Queue<Tuple2>(),
                answer = null
            };
        }
        public int CheckPuzzle(List<int> puzzle, List<int> board)
        {
            if (board.Count == 0)
            {
                board = null;
            }

            var tuple1 = SolveBoard(puzzle);

            if (tuple1.answer == null)
            {
                return -1;
            }

            if (board != null && !BoardMatches(board, tuple1.answer))
            {
                return -1;
            }

            var difficulty = tuple1.state.Count;
            var tuple2 = SolveNext(tuple1.state);

            if (tuple2.answer != null)
            {
                return -1;
            }

            return difficulty;
        }
        public bool BoardMatches(List<int> b1, List<int> b2)
        {
            for(var i = 0; i < b1.Count; i++)
            {
                if(b1[i] != b2[i])
                {
                    return false;
                }
            }

            return true;
        }
        public List<T> Shuffle<T>(List<T> Original)
        {
            for(int initialIndex = Original.Count - 1; initialIndex > 0; initialIndex--)
            {
                int targetIndex = RandomInt(initialIndex);
                T contents = Original[initialIndex];
                Original[initialIndex] = Original[targetIndex];
                Original[targetIndex] = contents;
            }

            return Original;
        }
        public int RandomInt(int Max)
        {
            var random = new Random();
            return random.Next(Max);
        }
        public void RemoveElement(ref List<Entry> List, int from, int to)
        {
            List.RemoveRange(from, to);
        }
        public List<int> BoardForEntries(List<Entry> Entries, int dimension)
        {
            var board = new List<int>(dimension * dimension);

            foreach(var entry in Entries)
            {
                board[entry.Position] = entry.Value;
            }

            return board;
        }

        #endregion
        #region Models

        public class Tuple1
        {
            public List<int> answer;
            public Queue<Tuple2> state;
        }
        public class Tuple2
        {
            public List<Entry> guesses;
            public int count;
            public List<int> board;
        }
        public class Entry
        {
            public int Position;
            public int Value;
        }
        public class Idk
        {
            public object Allowed;
            public object Needed;
        }

        #endregion
        #region Private Methods

        private List<int> InitializeKeyedIntList(int Size)
        {
            var List = new List<int>(Size);

            for (int index = 0; index < List.Count; index++)
            {
                List[index] = index;
            }

            return List;
        }
        private List<int> InitializeIntList(int Size)
        {
            var List = new List<int>(Size);

            for (int index = 0; index < List.Count; index++)
            {
                List[index] = 0;
            }

            return List;
        }

        #endregion
    }
    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
    }
}
