use std::collections::{hash_map::DefaultHasher, HashSet};
use std::fmt::Display;
use std::hash::{Hash, Hasher};

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
pub struct Point3D {
  pub x: i64,
  pub y: i64,
  pub z: i64,
}

impl Display for Point3D {
  fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
    write!(f, "({}, {}, {})", self.x, self.y, self.z)
  }
}

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
pub struct CircuitLink {
  pub from: Point3D,
  pub to: Point3D,
}

#[derive(Debug, Clone, PartialEq, Eq)]
pub struct Circuit {
  pub points: HashSet<CircuitLink>,
}

impl Hash for Circuit {
  fn hash<H: Hasher>(&self, state: &mut H) {
    // Order-independent combination: sum of element hashes + length
    let mut total: u64 = 0;
    for p in &self.points {
      let mut h = DefaultHasher::new();
      p.hash(&mut h);
      total = total.wrapping_add(h.finish());
    }
    state.write_u64(total);
    state.write_u64(self.points.len() as u64);
  }
}

impl Display for Circuit {
  fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
    // If the link is from a point to itself, indicate it's a single-point circuit like this: "{} to itself"
    let links: Vec<String> = self
      .points
      .iter()
      .map(|link| {
        if link.from == link.to {
          format!("{} to itself", link.from)
        } else {
          format!("{} -> {}", link.from, link.to)
        }
      })
      .collect();
    write!(f, "{}", links.join("\n"))
  }
}

pub fn find_euclidean_distance(a: &Point3D, b: &Point3D) -> f64 {
  let dx = (a.x - b.x) as f64;
  let dy = (a.y - b.y) as f64;
  let dz = (a.z - b.z) as f64;
  (dx * dx + dy * dy + dz * dz).sqrt()
}

pub fn closest_two_points_not_already_connected(
  input: &[Point3D],
  circuits: &[Circuit],
) -> Option<(Point3D, Point3D)> {
  let mut min_distance = f64::MAX;
  let mut closest_pair = None;

  for i in 0..input.len() {
    for j in (i + 1)..input.len() {
      let point_a = input[i];
      let point_b = input[j];

      let distance = find_euclidean_distance(&point_a, &point_b);
      // Skip pairs already in the same circuit
      if are_points_in_same_circuit(circuits, point_a, point_b) {
        continue;
      }

      if distance < min_distance {
        min_distance = distance;
        closest_pair = Some((point_a, point_b));
      }
    }
  }

  closest_pair
}

fn are_points_in_same_circuit(circuits: &[Circuit], point_a: Point3D, point_b: Point3D) -> bool {
  for circuit in circuits {
    let mut found_a = false;
    let mut found_b = false;
    for link in &circuit.points {
      if link.from == point_a || link.to == point_a {
        found_a = true;
      }
      if link.from == point_b || link.to == point_b {
        found_b = true;
      }
      if found_a && found_b {
        return true;
      }
    }
  }
  false
}

#[derive(Debug, Clone)]
pub struct CircuitUnionFind {
  parent: Vec<usize>,
  size: Vec<usize>,
}

impl CircuitUnionFind {
  /// Create a new CircuitUnionFind with `n` singleton nodes.
  pub fn new(n: usize) -> Self {
    CircuitUnionFind {
      parent: (0..n).collect(),
      size: vec![1; n],
    }
  }

  /// Find the root node for `node`.
  ///
  /// While locating the root we update each visited node so it points
  /// directly to the root. That shortens the chain of parents and makes
  /// future lookups faster.
  pub fn find_root(&mut self, node: usize) -> usize {
    if self.parent[node] != node {
      let root = self.find_root(self.parent[node]);
      self.parent[node] = root;
    }
    self.parent[node]
  }

  /// Unite the circuits containing `a` and `b`. Returns true if the two
  /// previously-separate circuits were merged.
  pub fn unite(&mut self, a: usize, b: usize) -> bool {
    let ra = self.find_root(a);
    let rb = self.find_root(b);
    if ra == rb {
      return false;
    }
    // attach smaller circuit under the larger one
    if self.size[ra] < self.size[rb] {
      self.parent[ra] = rb;
      self.size[rb] += self.size[ra];
    } else {
      self.parent[rb] = ra;
      self.size[ra] += self.size[rb];
    }
    true
  }

  /// Return the sizes of all circuits (connected components) as a vector.
  /// Each entry is the size (number of nodes) of a circuit.
  pub fn circuit_sizes(&mut self) -> Vec<usize> {
    let mut counts = vec![0usize; self.parent.len()];
    for i in 0..self.parent.len() {
      let r = self.find_root(i);
      counts[r] += 1;
    }
    counts.into_iter().filter(|&c| c > 0).collect()
  }
}

/// Build a list of candidate links between node indices and sort them by
/// increasing Euclidean distance. Each entry is `(index_a, index_b, distance)`.
/// The returned list is the same as "all unique links sorted by length".
pub fn create_sorted_link_list(points: &[Point3D]) -> Vec<(usize, usize, f64)> {
  let n = points.len();
  let mut links: Vec<(usize, usize, f64)> = Vec::with_capacity((n.saturating_sub(1) * n) / 2);
  for a in 0..n {
    for b in (a + 1)..n {
      let d = find_euclidean_distance(&points[a], &points[b]);
      links.push((a, b, d));
    }
  }
  links.sort_by(|x, y| match x.2.partial_cmp(&y.2) {
    Some(std::cmp::Ordering::Less) => std::cmp::Ordering::Less,
    Some(std::cmp::Ordering::Greater) => std::cmp::Ordering::Greater,
    Some(std::cmp::Ordering::Equal) => std::cmp::Ordering::Equal,
    None => std::cmp::Ordering::Greater,
  });
  links
}
