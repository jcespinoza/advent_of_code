use std::collections::HashSet;

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

pub fn generate_distinct_molecules(machine: &Machine) -> HashSet<String> {
  let mut distinct_molecules: HashSet<String> = HashSet::new();

  for replacement in machine.replacements.iter() {
    let mut global_pointer = 0;
    loop {
      let remaining_target = &machine.target_molecule[global_pointer..];
      let local_pos_option = remaining_target.find(&replacement.source);

      let local_pos = if let Some(pos) = local_pos_option {
        pos
      } else {
        break;
      };

      let width = replacement.source.len();
      let prefix = &machine.target_molecule[..global_pointer + local_pos];
      let suffix = &machine.target_molecule[global_pointer + local_pos + width..];

      let new_molecule = format!("{}{}{}", prefix, replacement.result, suffix);

      distinct_molecules.insert(new_molecule.clone());

      global_pointer += local_pos + width;
    }
  }

  distinct_molecules
}
