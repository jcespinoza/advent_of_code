#[derive(Debug, Clone)]
pub enum Instruction {
  Half(Register),
  Triple(Register),
  Increment(Register),
  Jump(i32),
  JumpIfEven(Register, i32),
  JumpIfOne(Register, i32),
}

#[derive(Debug, Clone)]
pub enum Register {
  A,
  B,
}

#[derive(Debug, Clone, PartialEq)]
pub enum State {
  Running,
  Halted,
}

#[derive(Debug, Clone)]
pub struct Computer {
  pub reg_a: i64,
  pub reg_b: i64,
  pub pointer: i32,
  pub state: State,
}

impl From<&str> for Instruction {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split_whitespace().collect();
    match parts[0] {
      "hlf" => Instruction::Half(parse_register(parts[1].chars().next().unwrap())),
      "tpl" => Instruction::Triple(parse_register(parts[1].chars().next().unwrap())),
      "inc" => Instruction::Increment(parse_register(parts[1].chars().next().unwrap())),
      "jmp" => Instruction::Jump(parts[1].parse().unwrap()),
      "jie" => Instruction::JumpIfEven(
        parse_register(parts[1].chars().next().unwrap()),
        parts[2].parse().unwrap(),
      ),
      "jio" => Instruction::JumpIfOne(
        parse_register(parts[1].chars().next().unwrap()),
        parts[2].parse().unwrap(),
      ),
      _ => panic!("Invalid instruction"),
    }
  }
}

fn parse_register(reg_name: char) -> Register {
  match reg_name {
    'a' => Register::A,
    'b' => Register::B,
    _ => panic!("Invalid register"),
  }
}

impl Default for Computer {
  fn default() -> Self {
    Computer {
      reg_a: 0,
      reg_b: 0,
      pointer: 0,
      state: State::Running,
    }
  }
}

impl Computer {
  pub fn run_program(&mut self, instructions: &[Instruction]) {
    while self.pointer >= 0 && self.pointer < instructions.len() as i32 {
      if self.state == State::Halted {
        break;
      }

      let instruction = &instructions[self.pointer as usize];
      match instruction {
        Instruction::Half(reg) => self.half(reg),
        Instruction::Triple(reg) => self.triple(reg),
        Instruction::Increment(reg) => self.increment(reg),
        Instruction::Jump(offset) => self.jump(*offset),
        Instruction::JumpIfEven(reg, offset) => self.jump_if_even(reg, *offset),
        Instruction::JumpIfOne(reg, offset) => self.jump_if_one(reg, *offset),
      }

      self.pointer += 1;

      let pointer_beyond_end = self.pointer >= instructions.len() as i32;
      let pointer_beyond_begin = self.pointer < 0_i32;
      if pointer_beyond_begin || pointer_beyond_end {
        self.state = State::Halted;
      }
    }
  }

  fn half(&mut self, reg: &Register) {
    match reg {
      Register::A => self.reg_a /= 2,
      Register::B => self.reg_b /= 2,
    }
  }

  fn triple(&mut self, reg: &Register) {
    match reg {
      Register::A => self.reg_a *= 3,
      Register::B => self.reg_b *= 3,
    }
  }

  fn increment(&mut self, reg: &Register) {
    match reg {
      Register::A => self.reg_a += 1,
      Register::B => self.reg_b += 1,
    }
  }

  fn jump(&mut self, offset: i32) {
    self.pointer += offset - 1;
  }

  fn jump_if_even(&mut self, reg: &Register, offset: i32) {
    let reg_value = self.get_register(reg);

    if reg_value % 2 == 0 {
      self.pointer += offset - 1;
    }
  }

  fn jump_if_one(&mut self, reg: &Register, offset: i32) {
    let reg_value = self.get_register(reg);

    if reg_value == 1 {
      self.pointer += offset - 1;
    }
  }

  fn get_register(&mut self, reg: &Register) -> i64 {
    match reg {
      Register::A => self.reg_a,
      Register::B => self.reg_b,
    }
  }
}
