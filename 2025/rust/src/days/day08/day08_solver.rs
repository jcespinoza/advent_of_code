#![allow(unused)]
use std::collections::HashMap;

use crate::{
  common::SteppedSolver,
  days::day08::{create_sorted_link_list, Circuit, CircuitLink, CircuitUnionFind, Point3D},
};

#[derive(Debug)]
pub struct Day08Solver {
  pub day: i32,
  pub year: i32,
  pub pairs_required: u32,
}

impl SteppedSolver<Vec<Point3D>, Vec<Point3D>, i64, i64> for Day08Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Point3D> {
    input
      .iter()
      .map(|line| {
        let coords: Vec<i64> = line
          .split(',')
          .map(|s| s.trim().parse::<i64>().unwrap())
          .collect();
        Point3D {
          x: coords[0],
          y: coords[1],
          z: coords[2],
        }
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Point3D> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, points: Vec<Point3D>) -> i64 {
    // We'll use a Disjoint Set Union (Union-Find) approach.
    let count_of_points = points.len();

    // Map each Point3D to its index for fast lookup
    let mut index_mapping: HashMap<Point3D, usize> = HashMap::with_capacity(count_of_points);
    for (index, point) in points.iter().enumerate() {
      index_mapping.insert(*point, index);
    }

    // Build sorted candidate links (index_i, index_j, distance) ascending by distance.
    let pairs = create_sorted_link_list(&points);

    let mut circuits = CircuitUnionFind::new(count_of_points);
    // Process the top-N shortest links (by distance). We treat them as the
    // chosen links (links = edges), even if some links join already-connected nodes.
    for &(i, j, _d) in pairs.iter().take(self.pairs_required as usize) {
      let _ = circuits.unite(i, j);
    }

    // compute circuit sizes
    let mut sizes = circuits.circuit_sizes();

    // Compute top-three component sizes and return their product
    sizes.sort_unstable_by(|a, b| b.cmp(a)); // Descending
    let top_three = &sizes[..3.min(sizes.len())];
    top_three.iter().map(|&s| s as i64).product()
  }

  fn solve_part_two(&self, points: Vec<Point3D>) -> i64 {
    let count_of_points = points.len();

    // Build sorted candidate links (index_i, index_j, distance) ascending by distance.
    let pairs = create_sorted_link_list(&points);

    let mut circuits = CircuitUnionFind::new(count_of_points);

    // compute circuit sizes
    let mut sizes = circuits.circuit_sizes();

    // Merge circuits until all points are connected
    // Keep track of the last link that caused a merge
    for &(i, j, _d) in &pairs {
      let merged = circuits.unite(i, j);
      if merged {
        sizes = circuits.circuit_sizes();
        if sizes.len() == 1 {
          // All points are now connected
          // Use the first two points that causes the circuits to merge into one
          return (points[i].x * points[j].x);
        }
      }
    }
    0
  }
}
