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

pub fn get_min_container_combinations(
  container_sizes: Vec<i32>,
  target_capacity: i32,
) -> Vec<Vec<i32>> {
  let good_combos = get_container_combinations(container_sizes, target_capacity);

  let smalles_combo_size = good_combos.iter().map(|x| x.len()).min().unwrap();
  let min_combos: Vec<Vec<i32>> = good_combos
    .into_iter()
    .filter(|x| x.len() == smalles_combo_size)
    .collect();

  min_combos
}
