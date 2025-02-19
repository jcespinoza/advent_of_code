#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day18::Day18Solver,
};

const DAY_NUM: i32 = 18;

#[test]
fn sample_1_step_part_one() {
  let raw_input = vec![".#.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.."];
  let expected_output: i64 = 11;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(1),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_2_step_part_one() {
  let raw_input = vec![".#.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.."];
  let expected_output: i64 = 8;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(2),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_3_step_part_one() {
  let raw_input = vec![".#.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.."];
  let expected_output: i64 = 4;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(3),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_4_step_part_one() {
  let raw_input = vec![".#.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.."];
  let expected_output: i64 = 4;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(4),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 814;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: None,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_1_step_part_two() {
  let raw_input = vec!["##.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.#"];
  let expected_output: i64 = 18;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(1),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_2_step_part_two() {
  let raw_input = vec!["##.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.#"];
  let expected_output: i64 = 18;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(2),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_3_step_part_two() {
  let raw_input = vec!["##.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.#"];
  let expected_output: i64 = 18;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(3),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_4_step_part_two() {
  let raw_input = vec!["##.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.#"];
  let expected_output: i64 = 14;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(4),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_5_step_part_two() {
  let raw_input = vec!["##.#.#", "...##.", "#....#", "..#...", "#.#..#", "####.#"];
  let expected_output: i64 = 17;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(5),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 924;

  let solver = Day18Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: None,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
