pub trait SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo> {
  fn parse_input_one(&self, input: Vec<&str>) -> TParsedInputOne;
  fn parse_input_two(&self, input: Vec<&str>) -> TParsedInputTwo;
  fn solve_part_one(&self, input: TParsedInputOne) -> TResultOne;
  fn solve_part_two(&self, input: TParsedInputTwo) -> TResultTwo;
}

pub trait HasDayYear {
  fn day(&self) -> i32;
  fn year(&self) -> i32;
}

pub trait SolverRunner {
  fn solve_part_one_str(&self, input: Vec<&str>) -> String;
  fn solve_part_two_str(&self, input: Vec<&str>) -> String;
}
