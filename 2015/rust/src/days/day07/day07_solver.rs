#![allow(unused)]
use std::collections::HashMap;

use crate::common::SteppedSolver;

use super::{Signal, Wire, Wiring};

#[derive(Debug)]
pub struct Day07Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Wire>, Vec<Wire>, i64, i64> for Day07Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Wire> {
    input.iter().map(|x| Wire::parse(x)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Wire> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, wirings: Vec<Wire>) -> i64 {
    let mut wire_map: HashMap<String, Wire> = HashMap::new();
    let mut known_wire_values: HashMap<String, u16> = HashMap::new();
    for wire in wirings {
      wire_map.insert(wire.name.clone(), wire);
    }

    let signal_in_wire: u16 = get_value_in_wire(&wire_map, "a", &mut known_wire_values);

    signal_in_wire as i64
  }

  fn solve_part_two(&self, wirings: Vec<Wire>) -> i64 {
    let part_one_result: u16 = self.solve_part_one(wirings.clone()).try_into().unwrap();

    let mut wire_map: HashMap<String, Wire> = HashMap::new();
    let mut known_wire_values: HashMap<String, u16> = HashMap::new();
    for wire in wirings {
      wire_map.insert(
        wire.name.clone(),
        match wire.name.as_str() {
          "b" => Wire {
            name: "b".to_string(),
            wiring: Wiring::DirectSignal(Signal::Value(part_one_result)),
          },
          _ => wire,
        },
      );
    }

    let signal_in_wire: u16 = get_value_in_wire(&wire_map, "a", &mut known_wire_values);

    signal_in_wire as i64
  }
}

fn get_value_in_wire(
  wire_map: &HashMap<String, Wire>,
  target_wire: &str,
  known_wires: &mut HashMap<String, u16>,
) -> u16 {
  if known_wires.contains_key(target_wire) {
    return *known_wires.get(target_wire).unwrap();
  }
  let wire = match wire_map.get(target_wire) {
    Some(wire) => wire,
    None => panic!("Wire not found: {}", target_wire),
  };
  let wiring = &wire.wiring;
  let value_found = match wiring {
    Wiring::DirectSignal(sig) => get_signal_in_wire(wire_map, sig, known_wires),
    Wiring::And(a, b) => {
      get_signal_in_wire(wire_map, a, known_wires) & get_signal_in_wire(wire_map, b, known_wires)
    }
    Wiring::Or(a, b) => {
      get_signal_in_wire(wire_map, a, known_wires) | get_signal_in_wire(wire_map, b, known_wires)
    }
    Wiring::LShift(a, b) => get_value_in_wire(wire_map, a, known_wires) << b,
    Wiring::RShift(a, b) => get_value_in_wire(wire_map, a, known_wires) >> b,
    Wiring::Not(a) => !get_signal_in_wire(wire_map, a, known_wires),
  };
  known_wires.insert(target_wire.to_string(), value_found);
  value_found
}

fn get_signal_in_wire(
  wire_map: &HashMap<String, Wire>,
  target_signal: &Signal,
  known_wires: &mut HashMap<String, u16>,
) -> u16 {
  match target_signal {
    Signal::Value(val) => *val,
    Signal::Wire(wire) => get_value_in_wire(wire_map, wire, known_wires),
  }
}
