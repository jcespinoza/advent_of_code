use std::collections::HashMap;

pub fn process_part1(input: &str) -> u32 {
    parse_heightmap(input);

    0
}

pub fn process_part2(input: &str) -> u32 {
    todo!()
}

fn parse_heightmap(input: &str) -> Input {
    let lines = input.lines();
    let mut start_coord = (0, 0);
    let mut end_coord = (0, 0);

    let heightmap: HashMap<Coord, u32> = lines
        .enumerate()
        .flat_map(|(line_index, line_item)| {
            line_item
                .chars()
                .enumerate()
                .map(move |(col_index, char_item)| {
                    let height_character = match char_item {
                        'S' => {
                            start_coord = (col_index as u32, line_index as u32);
                            // S has height a
                            'a'
                        }
                        'E' => {
                            end_coord = (col_index as u32, line_index as u32);
                            // E has height z
                            'z'
                        }
                        _ => char_item,
                    };
                    let height_value = height_character as u32 - 'a' as u32;
                    ((col_index as u32, line_index as u32), height_value)
                })
        })
        .collect();

    let input = Input {
        start: start_coord,
        end: end_coord,
        heightmap,
    };

    todo!()
}

type Coord = (u32, u32);

struct Input {
    start: Coord,
    end: Coord,
    heightmap: HashMap<Coord, u32>,
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, 31);
    }

    #[test]
    #[ignore]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 45000);
    }
}
