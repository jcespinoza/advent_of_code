pub enum UnitChange {
  Lose(i32),
  Gain(i32),
}

pub struct SeatingCondition {
  pub guest: String,
  pub neighbor: String,
  pub change: UnitChange,
}

impl From<&str> for SeatingCondition {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split_whitespace().collect();
    let guest = parts[0].to_string();
    let neighbor = parts[10].trim_end_matches('.').to_string();
    let units = parts[3].parse::<i32>().unwrap();
    let change = match parts[2] {
      "gain" => UnitChange::Gain(units),
      "lose" => UnitChange::Lose(units),
      _ => panic!("Invalid unit change"),
    };
    SeatingCondition {
      guest,
      neighbor,
      change,
    }
  }
}
