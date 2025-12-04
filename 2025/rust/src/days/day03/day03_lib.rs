pub struct BatteryBank {
  pub batteries: Vec<u8>,
}

use itertools::Itertools;

impl From<&str> for BatteryBank {
  fn from(s: &str) -> Self {
    let batteries = s.chars().map(|c| c.to_digit(10).unwrap() as u8).collect();
    BatteryBank { batteries }
  }
}

impl BatteryBank {
  // Calculates which N batteries provide the maximum total charge
  // The selected batteries must be returned in the same order as they appear in the bank
  pub fn top_n_batteries(&self, n: usize) -> Vec<u8> {
    // The total charge between the selected batteries is calculated by concatenating the digits
    // represented by the batteries and interpreting the result as a decimal number. Therefore sorting them first
    // is not enough to find which batteries to select. For example, in the bank 8912, 8 and 9 are the two largest batteries, but the best choice of two batteries is 9 and 2 which gives a total charge of 92
    // We need to generate all combinations of n batteries and find the one that gives the maximum charge
    let mut best_combination: Vec<u8> = Vec::new();
    let mut best_charge: i64 = -1;
    let len = self.batteries.len();
    let indices: Vec<usize> = (0..len).collect();
    // Use the `combinations` adapter from `Itertools` on the iterator of indices
    for combo in indices.iter().combinations(n) {
      // `combo` is a `Vec<&usize>`; copy the indices and select batteries preserving order
      let selected_batteries: Vec<u8> = combo
        .into_iter()
        .copied()
        .map(|i| self.batteries[i])
        .collect();
      let charge_str: String = selected_batteries.iter().map(|b| b.to_string()).collect();
      let charge = charge_str.parse::<i64>().unwrap();
      if charge > best_charge {
        best_charge = charge;
        best_combination = selected_batteries;
      }
    }
    best_combination
  }
}
