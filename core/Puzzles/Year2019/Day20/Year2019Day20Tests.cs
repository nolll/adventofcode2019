using NUnit.Framework;

namespace Core.Puzzles.Year2019.Day20;

public class Year2019Day20Tests
{
    [Test]
    public void FindsShortestRoute1()
    {
        const string input = @"
_                      _
_          A           _
_          A           _
_   #######.#########  _
_   #######.........#  _
_   #######.#######.#  _
_   #######.#######.#  _
_   #######.#######.#  _
_   #####  B    ###.#  _
_ BC...##  C    ###.#  _
_   ##.##       ###.#  _
_   ##...DE  F  ###.#  _
_   #####    G  ###.#  _
_   #########.#####.#  _
_ DE..#######...###.#  _
_   #.#########.###.#  _
_ FG..#########.....#  _
_   ###########.#####  _
_              Z       _
_              Z       _
_                      _";

        var solver = new DonutMazeSolver(input);

        Assert.That(solver.ShortestStepCount, Is.EqualTo(23));
    }

    [Test]
    public void FindsShortestRoute2()
    {
        const string input = @"
_                                     _
_                    A                _
_                    A                _
_   #################.#############   _
_   #.#...#...................#.#.#   _
_   #.#.#.###.###.###.#########.#.#   _
_   #.#.#.......#...#.....#.#.#...#   _
_   #.#########.###.#####.#.#.###.#   _
_   #.............#.#.....#.......#   _
_   ###.###########.###.#####.#.#.#   _
_   #.....#        A   C    #.#.#.#   _
_   #######        S   P    #####.#   _
_   #.#...#                 #......VT _
_   #.#.#.#                 #.#####   _
_   #...#.#               YN....#.#   _
_   #.###.#                 #####.#   _
_ DI....#.#                 #.....#   _
_   #####.#                 #.###.#   _
_ ZZ......#               QG....#..AS _
_   ###.###                 #######   _
_ JO..#.#.#                 #.....#   _
_   #.#.#.#                 ###.#.#   _
_   #...#..DI             BU....#..LF _
_   #####.#                 #.#####   _
_ YN......#               VT..#....QG _
_   #.###.#                 #.###.#   _
_   #.#...#                 #.....#   _
_   ###.###    J L     J    #.#.###   _
_   #.....#    O F     P    #.#...#   _
_   #.###.#####.#.#####.#####.###.#   _
_   #...#.#.#...#.....#.....#.#...#   _
_   #.#####.###.###.#.#.#########.#   _
_   #...#.#.....#...#.#.#.#.....#.#   _
_   #.###.#####.###.###.#.#.#######   _
_   #.#.........#...#.............#   _
_   #########.###.###.#############   _
_            B   J   C                _
_            U   P   P                _
_                                     _";

        var solver = new DonutMazeSolver(input);

        Assert.That(solver.ShortestStepCount, Is.EqualTo(58));
    }

    [Test]
    public void FindsShortestRouteRecursive()
    {
        const string input = @"
_                                               _
_              Z L X W       C                  _
_              Z P Q B       K                  _
_   ###########.#.#.#.#######.###############   _
_   #...#.......#.#.......#.#.......#.#.#...#   _
_   ###.#.#.#.#.#.#.#.###.#.#.#######.#.#.###   _
_   #.#...#.#.#...#.#.#...#...#...#.#.......#   _
_   #.###.#######.###.###.#.###.###.#.#######   _
_   #...#.......#.#...#...#.............#...#   _
_   #.#########.#######.#.#######.#######.###   _
_   #...#.#    F       R I       Z    #.#.#.#   _
_   #.###.#    D       E C       H    #.#.#.#   _
_   #.#...#                           #...#.#   _
_   #.###.#                           #.###.#   _
_   #.#....OA                       WB..#.#..ZH _
_   #.###.#                           #.#.#.#   _
_ CJ......#                           #.....#   _
_   #######                           #######   _
_   #.#....CK                         #......IC _
_   #.###.#                           #.###.#   _
_   #.....#                           #...#.#   _
_   ###.###                           #.#.#.#   _
_ XF....#.#                         RF..#.#.#   _
_   #####.#                           #######   _
_   #......CJ                       NM..#...#   _
_   ###.#.#                           #.###.#   _
_ RE....#.#                           #......RF _
_   ###.###        X   X       L      #.#.#.#   _
_   #.....#        F   Q       P      #.#.#.#   _
_   ###.###########.###.#######.#########.###   _
_   #.....#...#.....#.......#...#.....#.#...#   _
_   #####.#.###.#######.#######.###.###.#.#.#   _
_   #.......#.......#.#.#.#.#...#...#...#.#.#   _
_   #####.###.#####.#.#.#.#.###.###.#.###.###   _
_   #.......#.....#.#...#...............#...#   _
_   #############.#.#.###.###################   _
_                A O F   N                      _
_                A A D   M                      _
_                                               _";

        var solver = new RecursiveDonutMazeSolver(input);

        Assert.That(solver.ShortestStepCount, Is.EqualTo(396));
    }
}