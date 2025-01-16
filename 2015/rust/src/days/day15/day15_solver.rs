#![allow(unused)]
use crate::{common::SteppedSolver, days::day15::generate_partitions};

use super::{find_best_score, Ingredient};

#[derive(Debug)]
pub struct Day15Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Ingredient>, Vec<Ingredient>, i64, i64> for Day15Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Ingredient> {
    input.iter().map(|x| Ingredient::from(*x)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Ingredient> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, ingredients: Vec<Ingredient>) -> i64 {
    let partitions = generate_partitions(100, ingredients.len() as i32);
    let mut best_score: i64 = find_best_score(&partitions, &ingredients, None);

    best_score
  }

  fn solve_part_two(&self, ingredients: Vec<Ingredient>) -> i64 {
    let partitions = generate_partitions(100, ingredients.len() as i32);
    let mut best_score: i64 = find_best_score(&partitions, &ingredients, Some(500));

    best_score
  }
}
