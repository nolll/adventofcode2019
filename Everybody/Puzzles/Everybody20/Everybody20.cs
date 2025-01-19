using Pzl.Common;
using Pzl.Tools.CoordinateSystems.CoordinateSystem2D;

namespace Pzl.Everybody.Puzzles.Everybody20;

// Thanks to Alex Prosser
[Name("Gliding Finale")]
public class Everybody20 : EverybodyPuzzle
{
    private readonly Dictionary<char, MatrixDirection[]> _nextDirections = new()
    {
        { MatrixDirection.Up.Name, [MatrixDirection.Left, MatrixDirection.Up, MatrixDirection.Right] },
        { MatrixDirection.Right.Name, [MatrixDirection.Up, MatrixDirection.Right, MatrixDirection.Down] },
        { MatrixDirection.Down.Name, [MatrixDirection.Right, MatrixDirection.Down, MatrixDirection.Left] },
        { MatrixDirection.Left.Name, [MatrixDirection.Down, MatrixDirection.Left, MatrixDirection.Up] }
    };

    private readonly Dictionary<char, int> _changes = new()
    {
        { '.', -1 },
        { 'A', -1 },
        { 'B', -1 },
        { 'C', -1 },
        { '-', -2 },
        { '+', 1 }
    };

    public PuzzleResult Part1(string input)
    {
        var grid = MatrixBuilder.BuildCharMatrix(input);
        var s = grid.FindAddresses('S').First();
        grid.WriteValueAt(s, '.');
        var states = MatrixDirection.All.ToDictionary(k => (s.X, s.Y, k.Name), _ => 1000);

        for (var i = 0; i < 100; i++)
        {
            var next = new Dictionary<(int x, int y, char dir), int>();
            foreach (var state in states)
            {
                var (x, y, dir) = state.Key;
                var score = state.Value;
                grid.MoveTo(x, y);
                grid.TurnTo(MatrixDirection.Get(dir));
                foreach (var nextdir in _nextDirections[dir])
                {
                    grid.TurnTo(nextdir);
                    if (grid.TryMoveForward())
                    {
                        var v = grid.ReadValue();
                        if (v != '#')
                        {
                            var newScore = score + _changes[grid.ReadValue()];
                            var key = (grid.Address.X, grid.Address.Y, grid.Direction.Name);
                            if (next.TryGetValue(key, out var prevScore))
                                next[key] = Math.Max(newScore, prevScore);
                            else
                                next[key] = newScore;
                        }
                            
                        grid.MoveBackward();
                    }
                }
            }

            states = next;
        }

        var result = states.Values.Max();
        return new PuzzleResult(result, "ad1b597677b7daffac4e3c11d973c8be");
    }

    public PuzzleResult Part2(string input)
    {
        var grid = MatrixBuilder.BuildCharMatrix(input);
        var s = grid.FindAddresses('S').First();
        grid.WriteValueAt(s, '.');
        var states = MatrixDirection.All.ToDictionary(k => (s.X, s.Y, k.Name, ' '), _ => 10000);
        var time = 0;
        var found = false;
        
        while (!found)
        {
            time++;
            var next = new Dictionary<(int x, int y, char dir, char checkpoint), int>();
            foreach (var state in states)
            {
                var (x, y, dir, checkpoint) = state.Key;
                var score = state.Value;
                grid.MoveTo(x, y);
                grid.TurnTo(MatrixDirection.Get(dir));
                foreach (var nextdir in _nextDirections[dir])
                {
                    grid.TurnTo(nextdir);
                    if (grid.TryMoveForward())
                    {
                        var v = grid.ReadValue();
                        if (v != '#')
                        {
                            var newCheckpoint = checkpoint == ' ' && v == 'A' || 
                                                checkpoint == 'A' && v == 'B' || 
                                                checkpoint == 'B' && v == 'C'
                                ? v
                                : checkpoint;
                            var newScore = score + _changes[grid.ReadValue()];
                            if (newScore >= 10000 && grid.Address.X == s.X && grid.Address.Y == s.Y && newCheckpoint == 'C')
                                found = true;
                            var key = (grid.Address.X, grid.Address.Y, grid.Direction.Name, newCheckpoint);
                            if (next.TryGetValue(key, out var prevScore))
                                next[key] = Math.Max(newScore, prevScore);
                            else
                                next[key] = newScore;
                        }
                            
                        grid.MoveBackward();
                    }
                }
            }
            
            states = next;
        }
        
        return new PuzzleResult(time, "455f69d24dffb75a22302c1cbad1475b");
    }

    public PuzzleResult Part3(string input)
    {
        return new PuzzleResult(0);
    }
}