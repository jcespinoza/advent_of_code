#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day06::{Day06Solver, Operation, Problem},
};

const DAY_NUM: i32 = 6;

#[test]
fn sample_01_part_one() {
  let raw_input = vec![
    "123 328  51 64 ",
    " 45 64  387 23 ",
    "  6 98  215 314",
    "*   +   *   +  ",
  ];
  let expected_output: i64 = 4277556;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 6169101504608;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec![
    "123 328  51 64 ",
    " 45 64  387 23 ",
    "  6 98  215 314",
    "*   +   *   +  ",
  ];
  let expected_output: i64 = 3263827;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
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
  const EXPECTED_SOLUTION_PART2: i64 = 10442199710797;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
fn parsing_part_two() {
  let raw_input = vec![
    "123 328  51 64 ",
    " 45 64  387 23 ",
    "  6 98  215 314",
    "*   +   *   +  ",
  ];
  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);

  let target_problems: Vec<Problem> = vec![
    Problem {
      operation: Operation::Multiply,
      values: vec![1, 24, 356],
    },
    Problem {
      operation: Operation::Add,
      values: vec![369, 248, 8],
    },
    Problem {
      operation: Operation::Multiply,
      values: vec![32, 581, 175],
    },
    Problem {
      operation: Operation::Add,
      values: vec![623, 431, 4],
    },
  ];
  assert_eq!(input.problems.len(), target_problems.len());

  // compare each element in the grid to the target_problems values
  for (i, row) in input.problems.iter().enumerate() {
    for (j, &val) in row.values.iter().enumerate() {
      assert_eq!(val, target_problems[i].values[j]);
    }
  }
}
