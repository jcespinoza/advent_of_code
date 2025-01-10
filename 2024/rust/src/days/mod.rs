#![allow(unused_imports)]
pub mod day01 {
  pub use day01_impl::*;
  pub use day01_lib::*;
  pub use day01_solver::*;
  pub use day01_tests::*;

  mod day01_impl;
  mod day01_lib;
  mod day01_solver;
  mod day01_tests;
}
