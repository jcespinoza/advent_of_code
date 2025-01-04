using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day24Test : TestEngine<Day24Solver, Circuit, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 911832672L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;
        private readonly string[] EXAMPLE_ONE_2 = [
                                    "x00: 1",
                    "x01: 0",
                    "x02: 1",
                    "x03: 1",
                    "x04: 0",
                    "y00: 1",
                    "y01: 1",
                    "y02: 1",
                    "y03: 1",
                    "y04: 1",
                    "",
                    "ntg XOR fgs -> mjb",
                    "y02 OR x01 -> tnw",
                    "kwq OR kpj -> z05",
                    "x00 OR x03 -> fst",
                    "tgd XOR rvg -> z01",
                    "vdt OR tnw -> bfw",
                    "bfw AND frj -> z10",
                    "ffh OR nrd -> bqk",
                    "y00 AND y03 -> djm",
                    "y03 OR y00 -> psh",
                    "bqk OR frj -> z08",
                    "tnw OR fst -> frj",
                    "gnj AND tgd -> z11",
                    "bfw XOR mjb -> z00",
                    "x03 OR x00 -> vdt",
                    "gnj AND wpb -> z02",
                    "x04 AND y00 -> kjc",
                    "djm OR pbm -> qhw",
                    "nrd AND vdt -> hwm",
                    "kjc AND fst -> rvg",
                    "y04 OR y02 -> fgs",
                    "y01 AND x02 -> pbm",
                    "ntg OR kjc -> kwq",
                    "psh XOR fgs -> tgd",
                    "qhw XOR tgd -> z09",
                    "pbm OR djm -> kpj",
                    "x03 XOR y03 -> ffh",
                    "x00 XOR y04 -> ntg",
                    "bfw OR bqk -> z06",
                    "nrd XOR fgs -> wpb",
                    "frj XOR qhw -> z04",
                    "bqk OR frj -> z07",
                    "y03 OR x01 -> nrd",
                    "hwm AND bqk -> z03",
                    "tgd XOR rvg -> z12",
                    "tnw OR pbm -> gnj",
                ];
        private readonly string[] EXPECTED_EXAMPLE_ONE_2_WIREs = [
            "bfw: 1",
            "bqk: 1",
            "djm: 1",
            "ffh: 0",
            "fgs: 1",
            "frj: 1",
            "fst: 1",
            "gnj: 1",
            "hwm: 1",
            "kjc: 0",
            "kpj: 1",
            "kwq: 0",
            "mjb: 1",
            "nrd: 1",
            "ntg: 0",
            "pbm: 1",
            "psh: 1",
            "qhw: 1",
            "rvg: 0",
            "tgd: 0",
            "tnw: 1",
            "vdt: 1",
            "wpb: 0",
            "z00: 0",
            "z01: 0",
            "z02: 0",
            "z03: 1",
            "z04: 0",
            "z05: 1",
            "z06: 1",
            "z07: 1",
            "z08: 1",
            "z09: 1",
            "z10: 1",
            "z11: 0",
            "z12: 0",
            ];

        public Day24Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne
        {
            get
            {
                return new()
                {
                    ShouldSkipTests = false,
                    Example = new()
                    {
                        RawInput = [
                                    "x00: 1",
                    "x01: 1",
                    "x02: 1",
                    "y00: 0",
                    "y01: 1",
                    "y02: 0",
                    "",
                    "x00 AND y00 -> z00",
                    "x01 XOR y01 -> z01",
                    "x02 OR y02 -> z02",
                ],
                        Result = 4,
                    },
                    Examples = [new()
            {
                RawInput = EXAMPLE_ONE_2,
                Result = 2024,
            },
            ],
                    Solution = EXPECTED_SOLUTION_PART_1,
                };
            }
        }

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
                RawInput = [
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };

        [Fact]
        public void GateEvaluation_Test02()
        {
            var input = new Day24Solver().ParseInput(EXAMPLE_ONE_2);
            var expectedOuputs = EXPECTED_EXAMPLE_ONE_2_WIREs
                .Select(line => line.Split(": "))
                .Select(pair => new Wire { Name = pair[0], Value = pair[1] == "1" })
                .ToDictionary(w => w.Name, w => w.Value);

            var outputs = Day24Solver.EvaluateCircuit(input);

            outputs.Count.Should().Be(expectedOuputs.Count);

            foreach (var output in outputs.Values)
            {
                bool actualValue = output.Value;
                bool expectedValue = expectedOuputs[output.Name];

                actualValue.Should().Be(expectedValue);
            }
        }

        [Fact]
        public void BitInterpretation_Test01()
        {
            var input = new Day24Solver().ParseInput(EXAMPLE_ONE_2);
            var expectedOuput = "0011111101000";

            var outputs = Day24Solver.EvaluateCircuit(input);

            var zWires = outputs
                            .Where(o => o.Key.StartsWith("z"))
                            .ToList();
            var chars = zWires
                .Select(o => o.Value.Value ? '1' : '0');
            var reversed = chars.Reverse();
            var asString = string.Join("", reversed);

            asString.Should().Be(expectedOuput);
        }
    }
}