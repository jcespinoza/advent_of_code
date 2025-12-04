#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day02::Day02Solver,
};

const DAY_NUM: i32 = 2;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"];
  let expected_output: i64 = 1227775554;

  let solver = Day02Solver {
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
  const EXPECTED_SOLUTION_PART1: i64 = 31000881061;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec!["11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"];
  let expected_output: i64 = 4174379265;

  let solver = Day02Solver {
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
  const EXPECTED_SOLUTION_PART2: i64 = 46769308485;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["11-22"],
      expected_output: 33,
    },
    AocExample {
      raw_input: vec!["95-115"],
      expected_output: 99,
    },
    AocExample {
      raw_input: vec!["998-1012"],
      expected_output: 1010,
    },
    AocExample {
      raw_input: vec!["1188511880-1188511890"],
      expected_output: 1188511885,
    },
    AocExample {
      raw_input: vec!["222220-222224"],
      expected_output: 222222,
    },
    AocExample {
      raw_input: vec!["1698522-1698528"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["446443-446449"],
      expected_output: 446446,
    },
    AocExample {
      raw_input: vec!["38593856-38593862"],
      expected_output: 38593859,
    },
    AocExample {
      raw_input: vec!["565653-565659"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["824824821-824824827"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["2121212118-2121212124"],
      expected_output: 0,
    },
  ];

  let solver = Day02Solver {
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

#[test]
fn examples_part_two() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["11-22"],
      expected_output: 33,
    },
    AocExample {
      raw_input: vec!["95-115"],
      expected_output: 210,
    },
    AocExample {
      raw_input: vec!["998-1012"],
      expected_output: 2009,
    },
    AocExample {
      raw_input: vec!["1188511880-1188511890"],
      expected_output: 1188511885,
    },
    AocExample {
      raw_input: vec!["222220-222224"],
      expected_output: 222222,
    },
    AocExample {
      raw_input: vec!["1698522-1698528"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["446443-446449"],
      expected_output: 446446,
    },
    AocExample {
      raw_input: vec!["38593856-38593862"],
      expected_output: 38593859,
    },
    AocExample {
      raw_input: vec!["565653-565659"],
      expected_output: 565656,
    },
    AocExample {
      raw_input: vec!["824824821-824824827"],
      expected_output: 824824824,
    },
    AocExample {
      raw_input: vec!["2121212118-2121212124"],
      expected_output: 2121212121,
    },
  ];

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_two(example.raw_input.clone());
    let result = solver.solve_part_two(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}
