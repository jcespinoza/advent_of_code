using FluentAssertions;

using Xunit;

namespace AdventOfCode.Commons;

/// <summary>
/// Test engine for the implemented <typeparamref name="TSolver"/>
/// </summary>
///
/// <typeparam name="TSolver">
/// The <see cref="SteppedSolver{TInputOne, TInputTwo, TResult}"/> to use
/// </typeparam>
///
/// <typeparam name="TInputOne">
/// The type of the data used to solve the puzzle
/// </typeparam>
///
/// <typeparam name="TInputTwo">
/// The type of the data used to solve the puzzle
/// </typeparam>
///
/// <typeparam name="TResult">
/// The type of the result of the puzzle
/// </typeparam>
public abstract class SteppedTestEngine<TSolver, TInputOne, TInputTwo, TResult>
    where TSolver : SteppedSolver<TInputOne, TInputTwo, TResult>, new()
{
    /// <summary>
    /// The example used in the advent of code's subject and the expected result for it
    /// <para>
    /// This class is used to test your implementation of the first and second part of the puzzle
    /// </para>
    /// </summary>
    public record Example
    {  /// <summary>
       /// The input you would like to test the parser with
       /// </summary>
       /// <remarks>
       /// Leave empty if yuo do not wish to test the parsing
       /// </remarks>
        public string[] RawInput { get; init; } = [];

        /// <summary>
        /// The data of the input mentionned in the example, in the same form as the
        /// <see cref="Solver{TInputOne, TInputTwo, TResult}.PuzzleInput"/>
        /// </summary>
        public TInputOne InputOne { get; init; } = default!;

        /// <summary>
        /// The data of the input mentionned in the example, in the same form as the
        /// <see cref="Solver{TInputOne, TInputTwo, TResult}.PuzzleInput"/>
        /// </summary>
        public TInputTwo InputTwo { get; init; } = default!;

        /// <summary>
        /// The expected result for this example
        /// </summary>
        public TResult Result { get; init; } = default!;
    }

    /// <summary>
    /// The inputs to solve the given puzzle along with the expected solution
    /// </summary>
    public record Puzzle
    {
        /// <summary>
        /// If set to <c>true</c>, the tests for this puzzle will be skipped
        /// </summary>
        /// <remarks>
        /// Default is <c>false</c>
        /// </remarks>
        public bool ShouldSkipTests { get; init; } = false;

        /// <summary>
        /// The <see cref="Example"/> to use to test <see cref="Solver{TInputOne, TInputTwo, TResult}"/>
        /// </summary>
        public required Example Example { get; init; }

        /// <summary>
        /// The <see cref="Example"/>s to use to test <see cref="Solver{TInputOne, TInputTwo, TResult}"/>
        /// </summary>
        public Example[] Examples { get; set; } = [];

        /// <summary>
        /// The expected solution for the puzzle
        /// </summary>
        public required TResult Solution { get; init; }
    }

    /// <summary>
    /// The solver to use to solve the puzzle
    /// </summary>
    private readonly TSolver _solver;

    protected SteppedTestEngine() => _solver = new TSolver();

    #region Part #1

    public abstract Puzzle PartOne { get; }

    [SkippableFact(DisplayName = "Part One - Parsing")]
    public void PartOneParsingTest()
    {
        var shouldBeSkipped = PartOne.ShouldSkipTests
            || PartOne.Example.RawInput.Length == 0
            || PartOne.Example.InputOne == null;

        Skip.If(shouldBeSkipped, "Puzzle.ShouldSkipTests has been set to true or no raw input provided, test skipped");

        // Arrange
        var input = PartOne.Example.RawInput;

        // Act
        var result = _solver.ParseInputOne(input);

        // Assert
        result.Should().BeEquivalentTo(PartOne.Example.InputOne);
    }

    [SkippableFact(DisplayName = "Part One - Parsings")]
    public void PartOneParsingTests()
    {
        foreach (var example in PartOne.Examples)
        {
            var shouldBeSkipped = PartOne.ShouldSkipTests
            || example.RawInput.Length == 0
            || example.InputOne == null;

            Skip.If(shouldBeSkipped, "Puzzle.ShouldSkipTests has been set to true or no raw input provided, test skipped");

            // Arrange
            var input = example.RawInput;

            // Act
            var result = _solver.ParseInputOne(input);

            // Assert
            result.Should().BeEquivalentTo(example.InputOne);
        }
    }

    [SkippableFact(DisplayName = "Part One - Example")]
    public void PartOneExampleTest()
    {
        Skip.If(PartOne.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        // Arrange
        var input = PartOne.Example.InputOne ?? _solver.ParseInputOne(PartOne.Example.RawInput);

        // Act
        var result = _solver.PartOne(input);

        // Assert
        result.Should().Be(PartOne.Example.Result);
    }

    [SkippableFact(DisplayName = "Part One - Examples")]
    public void PartOneExamplesTest()
    {
        Skip.If(PartOne.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        foreach (var example in PartOne.Examples)
        {
            // Arrange
            var input = example.InputOne ?? _solver.ParseInputOne(example.RawInput);

            // Act
            var result = _solver.PartOne(input);

            // Assert
            result.Should().Be(example.Result);
        }
    }

    [SkippableFact(DisplayName = "Part One - Solution")]
    public void PartOneSolutionTest()
    {
        Skip.If(PartOne.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        // Arrange
        var input = _solver.PuzzleInputOne;

        // Act
        var result = _solver.PartOne(input.Value);

        // Assert
        result.Should().Be(PartOne.Solution);
    }

    #endregion

    #region Part #2

    public abstract Puzzle PartTwo { get; }

    [SkippableFact(DisplayName = "Part Two - Parsing")]
    public void PartTwoParsingTest()
    {
        var shouldBeSkipped = PartTwo.ShouldSkipTests
            || PartTwo.Example.RawInput.Length == 0
            || PartTwo.Example.InputTwo == null;

        Skip.If(shouldBeSkipped, "Puzzle.ShouldSkipTests has been set to true or no raw input provided, test skipped");

        // Arrange
        var input = PartTwo.Example.RawInput;

        // Act
        var result = _solver.ParseInputTwo(input);

        // Assert
        result.Should().BeEquivalentTo(PartTwo.Example.InputTwo);
    }

    [SkippableFact(DisplayName = "Part Two - Parsings")]
    public void PartTwoParsingTests()
    {
        foreach (var example in PartTwo.Examples)
        {
            var shouldBeSkipped = PartOne.ShouldSkipTests
            || example.RawInput.Length == 0
            || example.InputTwo == null;

            Skip.If(shouldBeSkipped, "Puzzle.ShouldSkipTests has been set to true or no raw input provided, test skipped");

            // Arrange
            var input = example.RawInput;

            // Act
            var result = _solver.ParseInputTwo(input);

            // Assert
            result.Should().BeEquivalentTo(example.InputTwo);
        }
    }

    [SkippableFact(DisplayName = "Part Two - Example")]
    public void PartTwoExampleTest()
    {
        Skip.If(PartTwo.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        // Arrange
        var input = PartTwo.Example.InputTwo ?? _solver.ParseInputTwo(PartTwo.Example.RawInput);

        // Act
        var result = _solver.PartTwo(input);

        // Assert
        result.Should().Be(PartTwo.Example.Result);
    }

    [SkippableFact(DisplayName = "Part Two - Examples")]
    public void PartTwoExamplesTest()
    {
        Skip.If(PartTwo.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        foreach (var example in PartTwo.Examples)
        {
            // Arrange
            var input = example.InputTwo ?? _solver.ParseInputTwo(example.RawInput);

            // Act
            var result = _solver.PartTwo(input);

            // Assert
            result.Should().Be(example.Result);
        }
    }

    [SkippableFact(DisplayName = "Part Two - Solution")]
    public void PartTwoSolutionTest()
    {
        Skip.If(PartTwo.ShouldSkipTests, "Puzzle.ShouldSkipTests has been set to true, test skipped");

        // Arrange
        var input = _solver.PuzzleInputTwo;

        // Act
        var result = _solver.PartTwo(input.Value);

        // Assert
        result.Should().Be(PartTwo.Solution);
    }

    #endregion
}
