﻿using AdventOfCode.Commons;
using Xunit;

namespace Advent24.Days
{
    public class Day24Solver : SteppedSolver<Circuit, Circuit, long, string>
    {
        public Day24Solver() : base(2024, 24) { }

        public override Circuit ParseInputOne(IEnumerable<string> input)
        {
            var inputList = input
                .TakeWhile(input => !string.IsNullOrEmpty(input))
                .Select(line => line.Split(": "))
                .Select(pair => new Wire { Name = pair[0], Value = pair[1] == "1" })
                .ToList();

            var gates = input
                .SkipWhile(input => !string.IsNullOrEmpty(input))
                .Skip(1)
                .Select(line => line.Split(" -> "))
                .Select(pair => new { Source = pair[0].Split(" "), Target = pair[1] })
                .Select(parts => new Gate
                {
                    InputA = parts.Source[0],
                    Operation = parts.Source[1] switch
                    {
                        "AND" => BoolOperation.AND,
                        "OR" => BoolOperation.OR,
                        "XOR" => BoolOperation.XOR,
                        _ => throw new Exception("Invalid operation")
                    },
                    InputB = parts.Source[2],
                    Target = parts.Target
                })
                .ToList();

            return new Circuit
            {
                Input = inputList
,
                Gates = gates
            };
        }
        public override Circuit ParseInputTwo(IEnumerable<string> input) => ParseInputOne(input);
        public override long PartOne(Circuit circuitSetup)
        {
            SortedList<string, Wire> outputs = EvaluateCircuit(circuitSetup);

            List<Wire> zWires = outputs
                .Where(o => o.Key.StartsWith('z'))
                .Select(o => o.Value).ToList();


            var chars = zWires
                .Select(o => o.Value ? '1' : '0');
            var reversed = chars.Reverse();
            var asString = string.Join("", reversed);

            long outputDecimal = Convert.ToInt64(asString, 2);

            return outputDecimal;
        }

        public static SortedList<string, Wire> EvaluateCircuit(Circuit circuitSetup)
        {
            Dictionary<string, bool> inputs = circuitSetup.Input.ToDictionary(wire => wire.Name, wire => wire.Value);
            Dictionary<string, Gate> gates = circuitSetup.Gates.ToDictionary(gate => gate.Target);

            SortedList<string, Wire> outputs = [];
            foreach (var gate in gates)
            {
                bool value = EvaluateGate(gate.Key, inputs, gates, outputs);
                Wire outputWire = new() { Name = gate.Key, Value = value };
                if (!outputs.ContainsKey(gate.Key))
                {
                    outputs.Add(gate.Key, outputWire);
                }
            }

            return outputs;
        }

        public static bool EvaluateGate(string outputName, Dictionary<string, bool> inputs, Dictionary<string, Gate> gates, SortedList<string, Wire> outputs)
        {
            if(outputName.StartsWith('x') || outputName.StartsWith('y'))
            {
                return inputs[outputName];
            }
            if(outputs.TryGetValue(outputName, out Wire existingOutput))
            {
                return existingOutput.Value;
            }
            Gate gate = gates[outputName];
            bool inputA = EvaluateGate(gate.InputA, inputs, gates, outputs);
            bool inputB = EvaluateGate(gate.InputB, inputs, gates, outputs);
            bool output = gate.Operation switch
            {
                BoolOperation.AND => inputA & inputB,
                BoolOperation.OR => inputA | inputB,
                BoolOperation.XOR => inputA ^ inputB,
                _ => throw new Exception("Invalid operation")
            };

            return output;
        }

        public override string PartTwo(Circuit circuitSetup)
        {
            throw new NotImplementedException();
        }
    }
}