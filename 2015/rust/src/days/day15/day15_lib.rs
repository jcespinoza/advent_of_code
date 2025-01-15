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
