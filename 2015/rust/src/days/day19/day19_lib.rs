#[derive(Debug, Clone, Hash, PartialEq)]
pub struct Replacement {
  pub source: String,
  pub result: String,
}

impl From<&str> for Replacement {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split(" => ").collect();
    Replacement {
      source: parts[0].to_string(),
      result: parts[1].to_string(),
    }
  }
}

#[derive(Debug, Clone, PartialEq)]
pub struct Machine {
  pub replacements: Vec<Replacement>,
  pub target_molecule: String,
}
