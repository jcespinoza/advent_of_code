use std::collections::HashSet;

use itertools::Itertools;

pub fn get_container_combinations(
  container_sizes: Vec<i32>,
  target_capacity: i32,
) -> Vec<Vec<i32>> {
  let mut good_combos: Vec<Vec<i32>> = vec![];

  for count in 1..container_sizes.len() {
    for combo in container_sizes.iter().combinations(count) {
      let sum_of_items: i32 = combo.iter().copied().sum();
      if sum_of_items == target_capacity {
        good_combos.push(combo.into_iter().copied().collect());
      }
    }
  }

  good_combos
}
