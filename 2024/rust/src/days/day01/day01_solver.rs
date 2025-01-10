use std::collections::HashMap;

use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day01Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<(i32, i32)>, Vec<(i32, i32)>, i64, i64> for Day01Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<(i32, i32)> {
    input
      .iter()
      .map(|x| {
        let parts = x.split_whitespace().collect::<Vec<&str>>();
        (parts[0].parse().unwrap(), parts[1].parse().unwrap())
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<(i32, i32)> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, pairs: Vec<(i32, i32)>) -> i64 {
    let mut left_ids: Vec<i32> = pairs.iter().map(|x| x.0).collect();
    let mut right_ids: Vec<i32> = pairs.iter().map(|x| x.1).collect();
    left_ids.sort();
    right_ids.sort();

    let mut differences: Vec<i64> = vec![];
    for (litem, ritem) in left_ids.iter().zip(right_ids.iter()) {
      let difference = ritem.abs_diff(*litem).into();
      differences.push(difference);
    }
    let final_sum: i64 = differences.iter().sum();
    final_sum
  }

  #[allow(unused)]
  fn solve_part_two(&self, pairs: Vec<(i32, i32)>) -> i64 {
    let mut right_ids: Vec<i32> = pairs.iter().map(|x| x.1).collect();
    let mut similarity_scores: Vec<i32> = vec![0; pairs.len()];
    let mut frequencies: HashMap<i32, i32> = HashMap::new();
    for (index, &item) in pairs.iter().enumerate() {
      let c_lefty = item.0;

      if frequencies.contains_key(&c_lefty) {
        similarity_scores[index] = c_lefty * *frequencies.get(&c_lefty).unwrap();
        continue;
      }

      // find how many times the right_ids array contain c_lefty
      let frequency = right_ids.iter().filter(|&&x| x == c_lefty).count() as i32;
      frequencies.insert(c_lefty, frequency);
      similarity_scores[index] = c_lefty * frequency;
    }
    let final_sum: i64 = similarity_scores.iter().map(|x| *x as i64).sum();
    final_sum
  }
}
