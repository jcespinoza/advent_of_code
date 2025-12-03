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
public abstract class SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo>
{   
    /// <summary>
    /// The puzzle input reader strategy
    /// </summary>
    public readonly IPuzzleInputReaderStrategy _puzzleInputReader;
    
    /// <summary>
    /// The puzzle input for Part One
    /// </summary>
    public readonly Lazy<TParsedInputOne> PuzzleInputOne;

    /// <summary>
    /// The puzzle input for Part Two
    /// </summary>
    public readonly Lazy<TParsedInputTwo> PuzzleInputTwo;

    /// <summary>
    /// Create a new solver and initialize its puzzle input based on the provided <see cref="IPuzzleInputReaderStrategy"/>
    /// </summary>
    /// <param name="puzzleInputReader">The strategy to use to retriever the puzzle input</param>
    private SteppedSolver(IPuzzleInputReaderStrategy puzzleInputReader)
    {
        _puzzleInputReader = puzzleInputReader;

        PuzzleInputOne = new Lazy<TParsedInputOne>(() =>
        {
            var content = puzzleInputReader.ReadInput();
            return ParseInputOne(content);
        });

        PuzzleInputTwo = new Lazy<TParsedInputTwo>(() =>
        {
            var content = puzzleInputReader.ReadInput();
            return ParseInputTwo(content);
        });
    }

    /// <summary>
    /// Create a new solver with a puzzle input located in a local file
    /// </summary>
    /// <param name="inputPath">The path to the file containing the input</param>
    protected SteppedSolver(string inputPath)
        : this(new LocalPuzzleInputReaderStrategy { InputPath = inputPath }) { }

    /// <summary>
    /// Create a new solver that will retrieve the puzzle input from the server
    /// </summary>
    /// <param name="year">The year of the current challenge</param>
    /// <param name="day">The day for which the puzzle input is needed</param>
    protected SteppedSolver(int year, int day)
        : this(new RemotePuzzleInputReaderStrategy { Year = year, Day = day }) { }


    /// <summary>
    /// Parse the input file and convert it to <typeparamref name="TParsedInputOne"/>
    /// for it to be used as the input of the <see cref="PartOne(TParsedInputOne)"/> method
    /// </summary>
    /// 
    /// <param name="inputPath">
    /// The content of the puzzle input
    /// </param>
    /// 
    /// <returns>
    /// The digested input
    /// </returns>
    public abstract TParsedInputOne ParseInputOne(IEnumerable<string> input);

    /// <summary>
    /// Parse the input file and convert it to <typeparamref name="TParsedInputTwo"/>
    /// for it to be used as the input of the <see cref="PartTwo(TParsedInputTwo)"/> method
    /// </summary>
    /// 
    /// <param name="inputPath">
    /// The content of the puzzle input
    /// </param>
    /// 
    /// <returns>
    /// The digested input
    /// </returns>
    public abstract TParsedInputTwo ParseInputTwo(IEnumerable<string> input);

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
    public abstract TResultOne PartOne(TParsedInputOne input);

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
    public abstract TResultTwo PartTwo(TParsedInputTwo input);
}
