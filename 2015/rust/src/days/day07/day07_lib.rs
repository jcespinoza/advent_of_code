use core::fmt;
use std::fmt::{format, Display, Formatter};

#[derive(Debug, PartialEq, Hash, Clone)]
pub enum Wiring {
  And(Signal, Signal),
  Or(Signal, Signal),
  LShift(String, u16),
  RShift(String, u16),
  Not(Signal),
  DirectSignal(Signal),
}

impl Display for Wiring {
  fn fmt(&self, f: &mut Formatter) -> fmt::Result {
    write!(
      f,
      "{}",
      match self {
        Wiring::And(a, b) => format!("{} AND {}", a, b),
        Wiring::Or(a, b) => format!("{} OR {}", a, b),
        Wiring::LShift(a, b) => format!("{} LSFHIT {}", a, b),
        Wiring::RShift(a, b) => format!("{} RSFHIT {}", a, b),
        Wiring::Not(a) => format!("NOT {}", a),
        Wiring::DirectSignal(a) => format!("{}", a),
      }
    )
  }
}

#[derive(Debug, PartialEq, Hash, Clone)]
pub enum Signal {
  Wire(String),
  Value(u16),
}

impl Signal {
  pub fn parse(input: &str) -> Self {
    if input.chars().all(char::is_numeric) {
      Self::Value(match input.parse() {
        Ok(v) => v,
        Err(_) => panic!("Invalid wiring. Expected a small number but got {}", input),
      })
    } else {
      Self::Wire(input.to_string())
    }
  }
}

impl Display for Signal {
  fn fmt(&self, f: &mut Formatter) -> fmt::Result {
    write!(
      f,
      "{}",
      match self {
        Signal::Wire(s) => s.to_string(),
        Self::Value(v) => v.to_string(),
      }
    )
  }
}

#[derive(Debug, PartialEq, Hash, Clone)]
pub struct Wire {
  pub name: String,
  pub wiring: Wiring,
}

impl Wire {
  pub fn new(name: String, wiring: Wiring) -> Self {
    Self { name, wiring }
  }

  pub fn parse(input: &str) -> Wire {
    let parts: Vec<&str> = input.split(" -> ").collect();
    let wiring = Wiring::parse(parts[0]);
    Wire {
      name: parts[1].to_string(),
      wiring,
    }
  }
}

impl Wiring {
  pub fn parse(input: &str) -> Self {
    let parts: Vec<&str> = input.split_whitespace().collect();
    match parts.len() {
      1 => {
        let a = parts[0];
        Self::DirectSignal(Signal::parse(a))
      }
      2 => {
        let a = parts[1];
        Self::Not(Signal::parse(a))
      }
      3 => {
        let a = parts[0];
        let b = parts[2];
        match parts[1] {
          "AND" => Self::And(Signal::parse(a), Signal::parse(b)),
          "OR" => Self::Or(Signal::parse(a), Signal::parse(b)),
          "LSHIFT" => Self::LShift(a.to_string(), b.parse().unwrap()),
          "RSHIFT" => Self::RShift(a.to_string(), b.parse().unwrap()),
          _ => panic!("Invalid wiring"),
        }
      }
      _ => panic!("Invalid wiring"),
    }
  }
}
