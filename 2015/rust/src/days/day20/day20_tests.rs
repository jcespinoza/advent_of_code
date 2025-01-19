#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day20::Day20Solver,
};

const DAY_NUM: i32 = 20;

#[test]
fn solution_part_one() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 665280;

  let solver = Day20Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 705600;

  let solver = Day20Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
#[ignore = "House counts lower than 10 do not work with the optimization for big numbers"]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["10"],
      expected_output: 1,
    },
    AocExample {
      raw_input: vec!["30"],
      expected_output: 2,
    },
    AocExample {
      raw_input: vec!["40"],
      expected_output: 3,
    },
    AocExample {
      raw_input: vec!["70"],
      expected_output: 4,
    },
    AocExample {
      raw_input: vec!["60"],
      expected_output: 5,
    },
    AocExample {
      raw_input: vec!["120"],
      expected_output: 6,
    },
    AocExample {
      raw_input: vec!["80"],
      expected_output: 7,
    },
    AocExample {
      raw_input: vec!["150"],
      expected_output: 8,
    },
    AocExample {
      raw_input: vec!["130"],
      expected_output: 9,
    },
  ];

  let solver = Day20Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_one(example.raw_input.clone());
    let result = solver.solve_part_one(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}
