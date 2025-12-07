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
