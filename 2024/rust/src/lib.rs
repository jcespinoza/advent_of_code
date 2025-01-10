pub mod common {
  pub use input_strategy::*;
  pub use solver::*;
  pub use testing::*;

  mod input_strategy;
  mod solver;
  mod testing;
}

pub mod days;

pub mod integration {
  pub mod solver_mapping;
}
