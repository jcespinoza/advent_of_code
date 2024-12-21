using AdventOfCode.Commons.PuzzleInputReaderStrategy;

namespace AdventOfCode.Commons;

/// <summary>
/// Represent the solver used for a given day
/// </summary>
/// 
/// <typeparam name="TInput">
/// The type of the data used to solve the puzzle
/// </typeparam>
/// 
/// <typeparam name="TResult">
/// The type of the result of the puzzle
/// </typeparam>
public abstract class Solver<TInput, TResult> : SteppedSolver<TInput, TInput, TResult>
{
    /// <summary>
    /// The puzzle input for Part One
    /// </summary>
    public readonly Lazy<TInput> PuzzleInput;

    /// <summary>
    /// Create a new solver with a puzzle input located in a local file
    /// </summary>
    /// <param name="inputPath">The path to the file containing the input</param>
    protected Solver(string inputPath) : base(inputPath) {
        PuzzleInput= new Lazy<TInput>(() =>
        {
            var content = _puzzleInputReader.ReadInput();
            return ParseInputOne(content);
        });
    }

    /// <summary>
    /// Create a new solver that will retrieve the puzzle input from the server
    /// </summary>
    /// <param name="year">The year of the current challenge</param>
    /// <param name="day">The day for which the puzzle input is needed</param>
    protected Solver(int year, int day) : base(year, day)
    {
        PuzzleInput= new Lazy<TInput>(() =>
        {
            var content = _puzzleInputReader.ReadInput();
            return ParseInputOne(content);
        });
    }


    /// <summary>
    /// Parse the input file and convert it to <typeparamref name="TInput"/>
    /// for it to be used as the input of the <see cref="PartOne(TInput)"/>
    /// and <see cref="PartTwo(TInput)"/>
    /// </summary>
    /// 
    /// <param name="inputPath">
    /// The path of your puzzle input
    /// </param>
    /// 
    /// <returns>
    /// The digested input
    /// </returns>
    public abstract TInput ParseInput(IEnumerable<string> input);

    public override TInput ParseInputOne(IEnumerable<string> input)
    {
        return ParseInput(input);
    }
    
    public override TInput ParseInputTwo(IEnumerable<string> input)
    {
        return ParseInput(input);
    }

    /// <summary>
    /// Logic for the solution of the first part of the puzzle
    /// </summary>
    /// 
    /// <param name="input">
    /// The digested puzzle input
    /// </param>
    /// 
    /// <returns>
    /// The puzzle's solution
    /// </returns>
    public override abstract TResult PartOne(TInput input);

    /// <summary>
    /// Logic for the solution of the second part of the puzzle
    /// </summary>
    /// 
    /// <param name="input">
    /// The digested puzzle input
    /// </param>
    /// 
    /// <returns>
    /// The puzzle's solution
    /// </returns>
    public override abstract TResult PartTwo(TInput input);
}
