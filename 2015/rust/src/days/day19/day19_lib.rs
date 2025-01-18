use std::collections::HashSet;

use itertools::Itertools;

#[derive(Debug, Clone, Hash, PartialEq, Eq)]
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

impl Replacement {
  pub fn inverse(&self) -> Replacement {
    Replacement {
      source: self.result.clone(),
      result: self.source.clone(),
    }
  }
}

#[derive(Debug, Clone, PartialEq)]
pub struct Machine {
  pub replacements: Vec<Replacement>,
  pub target_molecule: String,
}

pub fn common_parse(input: Vec<&str>, should_reverse: bool) -> Machine {
  // take all but the last two elements from the input
  let replacements = input
    .iter()
    .take(input.len() - 2)
    .map(|x| {
      let replacement = Replacement::from(*x);
      if should_reverse {
        return replacement.inverse();
      }
      replacement
    })
    .collect();

  // take the last element from the input
  let target_molecule = input[input.len() - 1].to_string();
  Machine {
    replacements,
    target_molecule,
  }
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

pub fn get_steps_until_target(replacements: &Vec<Replacement>, target_sequence: &str) -> i64 {
  if target_sequence == "e" {
    return 0;
  }

  let mut min_steps = i64::MAX;

  for replacement in replacements.iter() {
    let mut steps_required = i64::MAX;
    if target_sequence.contains(&replacement.source) {
      let rec_steps_required = get_steps_until_target(
        replacements,
        &target_sequence.replacen(&replacement.source, &replacement.result, 1),
      );
      if rec_steps_required < i64::MAX {
        steps_required = 1 + rec_steps_required;
      }
    }

    if steps_required < min_steps {
      min_steps = steps_required;
    }
  }

  min_steps
}
