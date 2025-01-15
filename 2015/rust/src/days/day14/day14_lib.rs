use std::collections::HashMap;

pub struct Reindeer {
  pub name: String,
  pub top_speed: i32,
  pub max_fly_time: i32,
  pub max_rest_time: i32,
}

pub struct Stat {
  pub distance: i32,
  pub points: i32,
  pub name: String,
  flying: bool,
  fly_time: i32,
  rest_time: i32,
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
      max_rest_time: rest_time,
    }
  }
}

pub fn compute_scores_after_seconds(
  all_reindeers: &[Reindeer],
  max_time: i32,
) -> HashMap<String, Stat> {
  let mut scores: HashMap<String, Stat> = HashMap::new();
  let mut current_second = 1;

  while current_second <= max_time {
    for reindeer in all_reindeers.iter() {
      initialize_stats_for_reindeer(&mut scores, reindeer);

      let deer_stat = scores.get_mut(&reindeer.name).unwrap();

      update_reindeer_stats(reindeer, deer_stat);
    }

    update_points(&mut scores);
    current_second += 1;
  }

  scores
}

fn update_reindeer_stats(reindeer: &Reindeer, deer_stat: &mut Stat) {
  if deer_stat.flying {
    deer_stat.distance += reindeer.top_speed;
    deer_stat.fly_time -= 1;
    if deer_stat.fly_time == 0 {
      deer_stat.flying = false;
      deer_stat.rest_time = reindeer.max_rest_time;
    }
  } else {
    deer_stat.rest_time -= 1;
    if deer_stat.rest_time == 0 {
      deer_stat.flying = true;
      deer_stat.fly_time = reindeer.max_fly_time;
    }
  }
}

fn initialize_stats_for_reindeer(scores: &mut HashMap<String, Stat>, reindeer: &Reindeer) {
  if !scores.contains_key(&reindeer.name) {
    scores.insert(
      reindeer.name.clone(),
      Stat {
        distance: 0,
        points: 0,
        flying: true,
        name: reindeer.name.clone(),
        fly_time: reindeer.max_fly_time,
        rest_time: 0,
      },
    );
  }
}

fn update_points(scores: &mut HashMap<String, Stat>) {
  let max_distance = scores.values().map(|s| s.distance).max().unwrap();
  // There could be a tie, so we need to increment the points to all reindeers with the max distance
  for (_, s) in scores.iter_mut() {
    if s.distance == max_distance {
      s.points += 1;
    }
  }
}
