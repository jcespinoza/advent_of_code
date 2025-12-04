/// A trait for solving problems with two parts, where each part may have different input parsing and result types.
/// This trait defines the necessary methods for parsing inputs and solving each part.
///
/// **TParsedInputOne**: The type of the parsed input for part one.
///
/// **TParsedInputTwo**: The type of the parsed input for part two.
///
/// **TResultOne**: The type of the result for part one.
///
/// **TResultTwo**: The type of the result for part two.
///
/// Example implementation:
///
/// ```ignore
/// impl SteppedSolver<Vec<i32>, Vec<i32>, i64, i64> for Day02Solver {
///   fn parse_input_one(&self, input: Vec<&str>) -> Vec<i32 { ... }
///   fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> { ... }
///   fn solve_part_one(&self, input: Vec<i32>) -> i64 { ... }
///   fn solve_part_two(&self, input: Vec<i32>) -> i64 { ... }
/// }
/// ```
pub trait SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo> {
  fn parse_input_one(&self, input: Vec<&str>) -> TParsedInputOne;
  fn parse_input_two(&self, input: Vec<&str>) -> TParsedInputTwo;
  fn solve_part_one(&self, input: TParsedInputOne) -> TResultOne;
  fn solve_part_two(&self, input: TParsedInputTwo) -> TResultTwo;
}

/// A trait to provide day and year information for a solver.
/// This is useful for identifying the specific problem being solved.
pub trait HasDayYear {
  fn day(&self) -> i32;
  fn year(&self) -> i32;
}

/// A trait for running solvers that produce string results for both parts of a problem.
/// This is useful for standardizing output formats.
pub trait SolverRunner {
  fn solve_part_one_str(&self, input: Vec<&str>) -> String;
  fn solve_part_two_str(&self, input: Vec<&str>) -> String;
}
