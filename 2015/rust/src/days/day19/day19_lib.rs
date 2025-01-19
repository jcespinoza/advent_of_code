use std::collections::{HashMap, HashSet};

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

pub fn get_steps_until_target(replacements: &[Replacement], target_sequence: &str) -> i64 {
  let replacement_map = replacements
    .iter()
    .fold(HashMap::<String, String>::new(), |mut acc, rep| {
      acc.insert(rep.source.to_string(), rep.result.to_string());
      acc
    });

  let mut sourted_outputs = replacement_map
    .keys()
    .map(|x| x.to_string())
    .collect::<Vec<String>>();

  sourted_outputs.sort_by_key(|b| std::cmp::Reverse(b.len()));

  let mut current_sequence = target_sequence.to_string();

  let mut step_count = 0;
  loop {
    for output in sourted_outputs.iter() {
      if current_sequence.contains(output) {
        let source = replacement_map.get(output).unwrap();
        current_sequence = current_sequence.replacen(output, source, 1);
        step_count += 1;
        break;
      }
    }
    if current_sequence == "e" {
      break;
    }
  }

  step_count
}
