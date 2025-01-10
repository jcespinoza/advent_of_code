use crate::{
  common::{PuzzleInputProvider, SteppedSolver},
  days::day01::Day01Solver,
};

pub struct AocExample<TResult> {
  pub expected_output: TResult,
  pub raw_input: String,
}

#[test]
fn test_sample_01_part_one() {
  let raw_input = vec!["3   4", "4   3", "2   5", "1   3", "3   9", "3   3"];
  let expected_output: i64 = 11;

  let solver = Day01Solver { day: 1, year: 2024 };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
// #[ignore]
fn test_solution_part_one() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(2024, 1)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 3714264;

  let solver = Day01Solver { day: 1, year: 2024 };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn test_sample_01_part_two() {
  let raw_input = vec!["3   4", "4   3", "2   5", "1   3", "3   9", "3   3"];
  let expected_output: i64 = 31;

  let solver = Day01Solver { day: 1, year: 2024 };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
// #[ignore]
fn test_solution_part_two() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(2024, 1)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 18805872;

  let solver = Day01Solver { day: 1, year: 2024 };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
