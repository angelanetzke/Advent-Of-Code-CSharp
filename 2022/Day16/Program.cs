﻿using Day16;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var connections = new Dictionary<string, string[]>();
	var valveCollection = new Dictionary<string, Valve>();
	var regex = new Regex(
		@"Valve (?<valveName>[A-Z]{2}) "
		+ @"has flow rate=(?<flowRate>\d+); "
		+ @"tunnel[s]? lead[s]? to valve[s]? (?<connectionList>[A-Z ,]+)");
	foreach (string thisLine in allLines)
	{
		var valveName = regex.Match(thisLine).Groups["valveName"].Value;
		var flowRate = int.Parse(regex.Match(thisLine).Groups["flowRate"].Value);
		var connectionList = regex.Match(thisLine).Groups["connectionList"].Value.Split(", ");
		connections[valveName] = connectionList;
		valveCollection[valveName] = new Valve(valveName, flowRate);
	}
	var timeLeft = 30;
	var startLocation = "AA";
	State.SetConnections(connections);
	var startState = new State(timeLeft, startLocation, valveCollection, false);
	var pressureReleased = startState.GetPressureReleased();	
	Console.WriteLine($"Part 1: {pressureReleased}");
}

static void Part2(string[] allLines)
{
	var connections = new Dictionary<string, string[]>();
	var valveCollection = new Dictionary<string, Valve>();
	var regex = new Regex(
		@"Valve (?<valveName>[A-Z]{2}) "
		+ @"has flow rate=(?<flowRate>\d+); "
		+ @"tunnel[s]? lead[s]? to valve[s]? (?<connectionList>[A-Z ,]+)");
	foreach (string thisLine in allLines)
	{
		var valveName = regex.Match(thisLine).Groups["valveName"].Value;
		var flowRate = int.Parse(regex.Match(thisLine).Groups["flowRate"].Value);
		var connectionList = regex.Match(thisLine).Groups["connectionList"].Value.Split(", ");
		connections[valveName] = connectionList;
		valveCollection[valveName] = new Valve(valveName, flowRate);
	}
	var timeLeft = 26;
	var startLocation = "AA";
	State.SetConnections(connections);
	State.SetTotalTime(timeLeft);
	var startState = new State(timeLeft, startLocation, valveCollection, true);
	var pressureReleased = startState.GetPressureReleased();
	Console.WriteLine($"Part 2: {pressureReleased}");
}
