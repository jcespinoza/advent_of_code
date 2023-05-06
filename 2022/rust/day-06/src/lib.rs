use std::collections::HashSet;

use nom::InputIter;

pub fn process_part1(input: &str) -> i32 {
    let marker_length = 4;
    return match find_marker(input, marker_length).unwrap() {
        result => return result as i32,
        _ => -1,
    };
}

fn find_marker(input: &str, marker_length: u32) -> Result<u32, String> {
    for i in 0..input.len() {
        let start_index: u32 = i as u32;
        let end_index: u32 = start_index + marker_length;
        let pre_marker = &input[start_index as usize..end_index as usize];

        if all_elements_unique(pre_marker) {
            return Ok(end_index as u32);
        }
    }
    Err("No marker found".to_string())
}

fn all_elements_unique(pre_marker: &str) -> bool {
    // check if all characters in slice are unique
    let mut unique = HashSet::new();
    pre_marker.iter_elements().all(move |x| unique.insert(x))
}

pub fn process_part2(input: &str) -> i32 {
  let marker_length = 14;
  return match find_marker(input, marker_length).unwrap() {
      result => return result as i32,
      _ => -1,
  };
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT_1: &str = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    const INPUT_2: &str = "bvwbjplbgvbhsrlpgdmjqwftvncz";
    const INPUT_3: &str = "nppdvjthqldpwncqszvftbrmjlhg";
    const INPUT_4: &str = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
    const INPUT_5: &str = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

    #[test]
    fn part1_1_works() {
        let result = process_part1(INPUT_1);
        assert_eq!(result, 7);
    }
    #[test]
    fn part1_2_works() {
        let result = process_part1(INPUT_2);
        assert_eq!(result, 5);
    }
    #[test]
    fn part1_3_works() {
        let result = process_part1(INPUT_3);
        assert_eq!(result, 6);
    }
    #[test]
    fn part1_4_works() {
        let result = process_part1(INPUT_4);
        assert_eq!(result, 10);
    }
    #[test]
    fn part1_5_works() {
        let result = process_part1(INPUT_5);
        assert_eq!(result, 11);
    }


    #[test]
    fn part2_1_works() {
        let result = process_part2(INPUT_1);
        assert_eq!(result, 19);
    }
    #[test]
    fn part2_2_works() {
        let result = process_part2(INPUT_2);
        assert_eq!(result, 23);
    }
    #[test]
    fn part2_3_works() {
        let result = process_part2(INPUT_3);
        assert_eq!(result, 23);
    }
    #[test]
    fn part2_4_works() {
        let result = process_part2(INPUT_4);
        assert_eq!(result, 29);
    }
    #[test]
    fn part2_5_works() {
        let result = process_part2(INPUT_5);
        assert_eq!(result, 26);
    }
}
