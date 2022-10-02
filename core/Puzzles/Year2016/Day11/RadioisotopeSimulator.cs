using System.Collections.Generic;
using System.Linq;
using Core.Common.Combinatorics;
using Core.Common.Strings;

namespace Core.Puzzles.Year2016.Day11;

public class RadioisotopeSimulator
{
    private readonly HashSet<string> _previousFacilities = new HashSet<string>();

    public int StepCount { get; }

    public RadioisotopeSimulator(string input)
    {
        var facility = ParseFacility(input);
        TrackVisit(facility);
        var finishedFacility = FindFinishedFacility(new List<RadioisotopeFacility> { facility });
        StepCount = finishedFacility?.IterationCount ?? 0;
    }

    private RadioisotopeFacility FindFinishedFacility(IList<RadioisotopeFacility> facilities)
    {
        var newFacilities = new List<RadioisotopeFacility>();
        foreach (var facility in facilities)
        {
            if (facility.ShouldMoveUp)
            {
                var itemCombinations = CombinationGenerator.GetAllCombinations(facility.Floors[facility.ElevatorFloor].Items, 2);
                var oldFloor = facility.ElevatorFloor;
                var newFloor = oldFloor + 1;
                foreach (var combination in itemCombinations)
                {
                    var f = new RadioisotopeFacility(facility, newFloor);
                    foreach (var item in combination)
                    {
                        f.Floors[oldFloor].Items.Remove(item);
                        f.Floors[newFloor].Items.Add(item);
                    }

                    if (!AlreadyVisited(f) && f.IsValid)
                    {
                        newFacilities.Add(f);
                        TrackVisit(f);
                    }
                }
            }

            if (facility.ShouldMoveDown)
            {
                var oldFloor = facility.ElevatorFloor;
                var newFloor = oldFloor - 1;

                foreach (var item in facility.Floors[facility.ElevatorFloor].Items)
                {
                    var f = new RadioisotopeFacility(facility, newFloor);
                    f.Floors[oldFloor].Items.Remove(item);
                    f.Floors[newFloor].Items.Add(item);

                    if (!AlreadyVisited(f) && f.IsValid)
                    {
                        newFacilities.Add(f);
                        TrackVisit(f);
                    }
                }
            }
        }

        if (!newFacilities.Any())
            return null;
        var finishedFacility = newFacilities.FirstOrDefault(o => o.IsDone);
        if (finishedFacility != null)
            return finishedFacility;
        var iterationCount = newFacilities.First().IterationCount;
        var facilityCount = newFacilities.Count;  
        return FindFinishedFacility(newFacilities);
    }

    private bool AlreadyVisited(RadioisotopeFacility f)
    {
        return _previousFacilities.Contains(f.AnonymizedId);
    }

    private void TrackVisit(RadioisotopeFacility f)
    {
        _previousFacilities.Add(f.AnonymizedId);
    }

    private RadioisotopeFacility ParseFacility(string input)
    {
        var strFloors = PuzzleInputReader.ReadLines(input);
        return new RadioisotopeFacility(strFloors.Select(ParseFloor).ToList(), 0);
    }

    private RadioisotopeFloor ParseFloor(string s)
    {
        s = s.Replace(" microchip", "-microchip").Replace(" generator", "-generator").Replace(",", "").Replace(".", "");
        var parts = s.Split(" ");
        var items = parts
            .Where(o => o.EndsWith("microchip") || o.EndsWith("generator"))
            .Select(CreateItem)
            .ToList();
        return new RadioisotopeFloor(items);
    }

    private RadioisotopeItem CreateItem(string s)
    {
        var parts = s.Split('-');
        var name = parts.First();
        var type = parts.Last();
        if (type == "microchip")
            return new Microchip(name);
        return new Generator(name);
    }
}