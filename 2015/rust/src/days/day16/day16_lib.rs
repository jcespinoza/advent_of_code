use std::fmt::Display;

use itertools::Itertools;

#[derive(Debug)]
pub struct AuntSue {
  pub number: i32,

  pub children: Option<i32>,
  pub cats: Option<i32>,
  pub samoyeds: Option<i32>,
  pub pomeranians: Option<i32>,
  pub akitas: Option<i32>,
  pub vizslas: Option<i32>,
  pub goldfish: Option<i32>,
  pub trees: Option<i32>,
  pub cars: Option<i32>,
  pub perfumes: Option<i32>,
}

pub struct MachineResult {
  pub children: i32,
  pub cats: i32,
  pub samoyeds: i32,
  pub pomeranians: i32,
  pub akitas: i32,
  pub vizslas: i32,
  pub goldfish: i32,
  pub trees: i32,
  pub cars: i32,
  pub perfumes: i32,
}

impl From<&str> for AuntSue {
  fn from(input: &str) -> Self {
    let mut sue = AuntSue {
      number: 0,
      children: None,
      cats: None,
      samoyeds: None,
      pomeranians: None,
      akitas: None,
      vizslas: None,
      goldfish: None,
      trees: None,
      cars: None,
      perfumes: None,
    };

    let parts: Vec<&str> = input.split_whitespace().collect();
    sue.number = parts[1].trim_end_matches(':').parse::<i32>().unwrap();

    for i in (2..parts.len()).step_by(2) {
      let key = parts[i].trim_end_matches(':');
      let value = parts[i + 1].trim_end_matches(',');
      update_sue(&mut sue, key, value.parse::<i32>().unwrap());
    }

    sue
  }
}

impl Display for AuntSue {
  fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
    write!(
      f,
      "Sue {}: {}",
      self.number,
      [
        ("children", self.children),
        ("cats", self.cats),
        ("samoyeds", self.samoyeds),
        ("pomeranians", self.pomeranians),
        ("akitas", self.akitas),
        ("vizslas", self.vizslas),
        ("goldfish", self.goldfish),
        ("trees", self.trees),
        ("cars", self.cars),
        ("perfumes", self.perfumes),
      ]
      .iter()
      .filter(|x| x.1.is_some())
      .map(|x| format!("{}: {}", x.0, x.1.unwrap_or(-1)))
      .collect::<Vec<String>>()
      .join(", ")
    )
  }
}

fn update_sue(sue: &mut AuntSue, key: &str, value: i32) {
  match key {
    "children" => sue.children = Some(value),
    "cats" => sue.cats = Some(value),
    "samoyeds" => sue.samoyeds = Some(value),
    "pomeranians" => sue.pomeranians = Some(value),
    "akitas" => sue.akitas = Some(value),
    "vizslas" => sue.vizslas = Some(value),
    "goldfish" => sue.goldfish = Some(value),
    "trees" => sue.trees = Some(value),
    "cars" => sue.cars = Some(value),
    "perfumes" => sue.perfumes = Some(value),
    _ => panic!("Unknown key: {}", key),
  }
}

pub fn find_aunt(aunts: &[AuntSue], machine_result: &MachineResult, use_exact: bool) -> i64 {
  let mut matching_aunts = Vec::new();
  for aunt in aunts.iter() {
    if aunt.children.is_some() && aunt.children.unwrap() != machine_result.children {
      continue;
    }
    if aunt.cats.is_some() && aunt.cats.unwrap() != machine_result.cats {
      continue;
    }
    if aunt.samoyeds.is_some() && aunt.samoyeds.unwrap() != machine_result.samoyeds {
      continue;
    }
    if aunt.pomeranians.is_some() && aunt.pomeranians.unwrap() != machine_result.pomeranians {
      continue;
    }
    if aunt.akitas.is_some() && aunt.akitas.unwrap() != machine_result.akitas {
      continue;
    }
    if aunt.vizslas.is_some() && aunt.vizslas.unwrap() != machine_result.vizslas {
      continue;
    }
    if aunt.goldfish.is_some() && aunt.goldfish.unwrap() != machine_result.goldfish {
      continue;
    }
    if aunt.trees.is_some() && aunt.trees.unwrap() != machine_result.trees {
      continue;
    }
    if aunt.cars.is_some() && aunt.cars.unwrap() != machine_result.cars {
      continue;
    }
    if aunt.perfumes.is_some() && aunt.perfumes.unwrap() != machine_result.perfumes {
      continue;
    }

    matching_aunts.push(aunt);
  }

  if matching_aunts.len() == 1 {
    return matching_aunts[0].number as i64;
  } else {
    println!(
      "Matching aunts: {:?}",
      matching_aunts.iter().map(|x| x.to_string()).join("\n")
    );
  }

  panic!("Could not find aunt");
}
