// region: defaul_implementations
pub mod day01 {
  use crate::{
    common::solver::{HasDayYear, SolverRunner, SteppedSolver},
    days::day01::day01_solver::day01::Day01Solver,
  };

  impl HasDayYear for Day01Solver {
    fn day(&self) -> i32 {
      self.day
    }

    fn year(&self) -> i32 {
      self.year
    }
  }

  impl SolverRunner for Day01Solver {
    fn solve_part_one_str(&self, input: Vec<&str>) -> String {
      let parsed_input = self.parse_input_one(input);
      self.solve_part_one(parsed_input).to_string()
    }

    fn solve_part_two_str(&self, input: Vec<&str>) -> String {
      let parsed_input = self.parse_input_two(input);
      self.solve_part_two(parsed_input).to_string()
    }
  }
}
// endregion
