pub struct BatteryBank {
  pub batteries: Vec<u8>,
}

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
    // Efficient selection: choose the lexicographically largest subsequence of length `n`
    // while preserving order. Concatenation of single-digit numbers corresponds to
    // lexicographic order for equal-length sequences, so a greedy scan works:
    // for each slot pick the largest digit available within the window that leaves
    // enough elements to fill the remaining slots.
    let len = self.batteries.len();
    if n >= len {
      // If n is greater than or equal to the number of batteries, return all batteries
      return self.batteries.clone();
    }

    let mut result: Vec<u8> = Vec::with_capacity(n);
    let mut start: usize = 0;
    let mut remaining = n;

    while remaining > 0 {
      // `max_pos` is the last index we may pick for this slot so that
      // there are still `remaining - 1` elements left for the following slots.
      let max_pos = len - remaining; // inclusive

      // `best_idx` will record the index of the best digit found in the window.
      let mut best_idx = start;

      // `best_val` is the digit at `best_idx`; initialize to the first candidate.
      let mut best_val = self.batteries[start];

      // Scan the window from `start` to `max_pos` (inclusive) to find the maximum digit.
      for i in start..=max_pos {
        // current digit at position `i`.
        let v = self.batteries[i];

        // If this digit is greater than the best seen so far, update the best.
        if v > best_val {
          best_val = v;
          best_idx = i;

          // If we found a 9, it is the maximum possible digit — stop scanning early.
          if best_val == 9 {
            break; // early exit, can't do better than 9
          }
        }
      }

      // Append the chosen best digit for this slot to the result.
      result.push(best_val);

      // Move `start` to the next element after the chosen index so we preserve order.
      start = best_idx + 1;

      // One less slot to fill.
      remaining -= 1;
    }

    result
  }
}
