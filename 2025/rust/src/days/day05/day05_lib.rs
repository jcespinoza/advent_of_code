/// Ingredient database
/// Holds a list of ranges of IDs of the ingredients that are fresh and a list of
/// IDs of ingredients that are available
#[derive(Debug, Clone)]
pub struct IngrendientDb {
  pub fresh_id_ranges: Vec<(i64, i64)>,
  pub available_ids: Vec<i64>,
}

/// Implementation of the From trait to create an IngredientDb from a Vec<&str>
/// The input will be a Vec of strings where the first section contains ranges of fresh ingredient IDs
/// in the format "start-end" (one per line), followed by one str that is just an empty string,
/// and then a list of available ingredient IDs (one per line).
impl From<Vec<&str>> for IngrendientDb {
  fn from(input: Vec<&str>) -> Self {
    let mut fresh_id_ranges = Vec::new();
    let mut available_ids = Vec::new();
    let mut parsing_ranges = true;

    for line in input {
      if line.is_empty() {
        parsing_ranges = false;
        continue;
      }

      if parsing_ranges {
        let parts: Vec<&str> = line.split('-').collect();
        if parts.len() == 2 {
          if let (Ok(start), Ok(end)) = (parts[0].parse::<i64>(), parts[1].parse::<i64>()) {
            fresh_id_ranges.push((start, end));
          } else {
            panic!("Invalid range: {}", line);
          }
        }
      } else if let Ok(id) = line.parse::<i64>() {
        available_ids.push(id);
      } else {
        // Handle parsing error if necessary
        panic!("Invalid available ID: {}", line);
      }
    }

    IngrendientDb {
      fresh_id_ranges,
      available_ids,
    }
  }
}

/// Returns a new list of ranges that merges overlapping and contiguous ranges
pub fn merge_ranges(ranges: &Vec<(i64, i64)>) -> Vec<(i64, i64)> {
  if ranges.is_empty() {
    return Vec::new();
  }

  let mut sorted_ranges = ranges.clone();
  // Sort ranges by their start value so we can merge in a single pass.
  sorted_ranges.sort_by_key(|k| k.0);

  // `merged_ranges` will collect the resulting non-overlapping ranges.
  let mut merged_ranges = Vec::new();

  // Start with the first (lowest-start) range as the current one to compare against.
  let mut current_range = sorted_ranges[0];

  // Iterate the remaining ranges and merge them into `current_range` when they
  // overlap or are contiguous (i.e., the next start is <= current end + 1).
  for &range in sorted_ranges.iter().skip(1) {
    // If the next range starts before or immediately after the current range ends,
    // extend the `current_range` to include it (take the max end).
    if range.0 <= current_range.1 + 1 {
      current_range.1 = current_range.1.max(range.1);
    } else {
      // Otherwise, the ranges are disjoint: push the finished `current_range`
      // and begin a new one.
      merged_ranges.push(current_range);
      current_range = range;
    }
  }

  // Push the last accumulated range and return the merged list.
  merged_ranges.push(current_range);
  merged_ranges
}
