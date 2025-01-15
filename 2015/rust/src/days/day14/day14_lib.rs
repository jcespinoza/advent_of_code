use std::collections::HashMap;

pub struct Reindeer {
  pub name: String,
  pub top_speed: i32,
  pub max_fly_time: i32,
  pub rest_time: i32,
}

pub struct Score {
  pub distance: i32,
  pub points: i32,
}

impl From<&str> for Reindeer {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split_whitespace().collect();
    let name = parts[0].to_string();
    let top_speed = parts[3].parse().unwrap();
    let max_fly_time = parts[6].parse().unwrap();
    let rest_time = parts[13].parse().unwrap();

    Reindeer {
      name,
      top_speed,
      max_fly_time,
      rest_time,
    }
  }
}

pub fn compute_distances_after_seconds(
  all_reindeers: &[Reindeer],
  max_time: i32,
) -> HashMap<String, i32> {
  let mut distances: HashMap<String, i32> = HashMap::new();

  for reindeer in all_reindeers.iter() {
    let mut distance = 0;
    let mut fly_time = reindeer.max_fly_time;
    let mut rest_time = 0;
    let mut flying = true;

    let mut current_second = 1;
    while current_second <= max_time {
      if flying {
        distance += reindeer.top_speed;
        fly_time -= 1;
        if fly_time == 0 {
          flying = false;
          rest_time = reindeer.rest_time;
        }
      } else {
        rest_time -= 1;
        if rest_time == 0 {
          flying = true;
          fly_time = reindeer.max_fly_time;
        }
      }

      current_second += 1;
    }

    distances.insert(reindeer.name.clone(), distance);
  }

  distances
}

pub fn compute_scores_after_seconds(
  all_reindeers: &[Reindeer],
  max_time: i32,
) -> HashMap<String, Score> {
  let mut scores: HashMap<String, Score> = HashMap::new();
  todo!()
}
