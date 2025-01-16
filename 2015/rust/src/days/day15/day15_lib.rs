use std::collections::HashSet;

use itertools::Itertools;

pub struct Ingredient {
  pub name: String,
  pub capacity: i32,
  pub durability: i32,
  pub flavor: i32,
  pub texture: i32,
  pub calories: i32,
}

impl From<&str> for Ingredient {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split_whitespace().collect();
    let name = parts[0].trim_end_matches(':');
    let capacity = parts[2].trim_end_matches(',').parse().unwrap();
    let durability = parts[4].trim_end_matches(',').parse().unwrap();
    let flavor = parts[6].trim_end_matches(',').parse().unwrap();
    let texture = parts[8].trim_end_matches(',').parse().unwrap();
    let calories = parts[10].parse().unwrap();
    Ingredient {
      name: name.to_string(),
      capacity,
      durability,
      flavor,
      texture,
      calories,
    }
  }
}

pub fn generate_partitions(target_sum: i32, num_parts: i32) -> Vec<Vec<i32>> {
  let mut partitions = vec![];
  let mut seen = HashSet::new();
  if num_parts == 1 {
    partitions.push(vec![target_sum]);
  } else {
    for i in 0..target_sum {
      let mut sub_partitions = generate_partitions(target_sum - i, num_parts - 1);
      for partition in sub_partitions.iter_mut() {
        partition.push(i);
        partition.sort(); // Sort to ensure uniqueness
        if seen.insert(partition.clone()) {
          partitions.push(partition.clone());
        }
      }
    }
  }
  partitions.retain(|x| x.iter().all(|&y| y != 0));
  partitions
}

pub fn find_best_score(
  partitions: &[Vec<i32>],
  ingredients: &[Ingredient],
  required_calories: Option<i32>,
) -> i64 {
  let mut max_score: i64 = 0;

  for partition in partitions.iter() {
    for permutation in partition.iter().permutations(partition.len()) {
      let mut capacity = 0;
      let mut durability = 0;
      let mut flavor = 0;
      let mut texture = 0;
      let mut calories = 0;

      for (i, amount) in permutation.iter().enumerate() {
        capacity += *amount * ingredients[i].capacity;
        durability += *amount * ingredients[i].durability;
        flavor += *amount * ingredients[i].flavor;
        texture += *amount * ingredients[i].texture;
        calories += *amount * ingredients[i].calories;
      }

      if let Some(r_calories) = required_calories {
        if calories != r_calories {
          continue;
        }
      }

      if capacity < 0 || durability < 0 || flavor < 0 || texture < 0 {
        continue;
      }

      let score = (capacity * durability * flavor * texture) as i64;

      if score > max_score {
        max_score = score;
      }
    }
  }

  max_score
}
