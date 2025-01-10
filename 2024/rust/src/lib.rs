pub mod common {
  pub use input_strategy::*;
  pub use solver::*;

  mod input_strategy;
  mod solver;
}

pub mod days;

pub mod integration {
  pub mod solver_mapping;
}
