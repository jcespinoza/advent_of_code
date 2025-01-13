use itertools::Itertools;

use crate::common::SteppedSolver;

use super::Segment;

#[derive(Debug)]
pub struct Day09Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Segment>, Vec<Segment>, i64, i64> for Day09Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Segment> {
    input
      .iter()
      .map(|&line| {
        let parts: Vec<&str> = line.split(" = ").collect();
        let cities: Vec<&str> = parts[0].split(" to ").collect();
        let distance: i32 = parts[1].parse().unwrap();
        Segment {
          from: cities[0].to_owned(),
          to: cities[1].to_owned(),
          distance,
        }
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Segment> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<Segment>) -> i64 {
    let mut cities = std::collections::HashSet::new();
    let mut distances = std::collections::HashMap::new();
    for segment in input.iter() {
      cities.insert(segment.from.clone());
      cities.insert(segment.to.clone());
      distances.insert((segment.from.clone(), segment.to.clone()), segment.distance);
      distances.insert((segment.to.clone(), segment.from.clone()), segment.distance);
    }

    let mut shortest_distance = i32::MAX;

    for route_permutation in cities.iter().permutations(cities.len()) {
      let mut total_route_distance = 0;
      for i in 0..route_permutation.len() - 1 {
        let c_start = route_permutation[i].clone();
        let c_target = route_permutation[i + 1].clone();
        total_route_distance += distances.get(&(c_start, c_target)).unwrap();
      }
      if total_route_distance < shortest_distance {
        shortest_distance = total_route_distance;
      }
    }

    shortest_distance as i64
  }

  fn solve_part_two(&self, input: Vec<Segment>) -> i64 {
    unimplemented!()
  }
}
