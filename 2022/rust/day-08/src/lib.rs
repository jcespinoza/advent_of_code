pub fn process_part1(input: &str) -> u32 {
    let grid_of_trees = parse_grid_of_trees(input);
    count_visible_trees(grid_of_trees)
}

fn count_visible_trees(grid_of_trees: TreeGrid) -> u32 {
    let edge_visible_trees = 2 * (grid_of_trees.rows - 2 + grid_of_trees.columns);
    let mut interior_visible_trees = 0;

    for row in 1..grid_of_trees.rows - 1 {
        for col in 1..grid_of_trees.columns - 1 {
            let current_tree = grid_of_trees.trees[row as usize][col as usize];

            if is_visible_from_above(row, &grid_of_trees, col, current_tree)
                || is_visible_from_left(col, &grid_of_trees, row, current_tree)
                || is_visible_from_below(row, &grid_of_trees, col, current_tree)
                || is_visible_from_right(col, &grid_of_trees, row, current_tree)
            {
                interior_visible_trees += 1;
            }
        }
    }
    println!(
        "Edge: {}, Interior: {}",
        edge_visible_trees, interior_visible_trees
    );
    edge_visible_trees + interior_visible_trees
}

fn is_visible_from_above(row: u32, grid_of_trees: &TreeGrid, col: u32, current_tree: u32) -> bool {
    for row_offset in 0..row {
        let top_tree = grid_of_trees.trees[row_offset as usize][col as usize];
        if top_tree >= current_tree {
            return false;
        }
    }
    true
}

fn is_visible_from_below(row: u32, grid_of_trees: &TreeGrid, col: u32, current_tree: u32) -> bool {
    for row_offset in (row + 1)..grid_of_trees.rows {
        let bottom_tree = grid_of_trees.trees[row_offset as usize][col as usize];
        if bottom_tree >= current_tree {
            return false;
        }
    }
    true
}

fn is_visible_from_left(col: u32, grid_of_trees: &TreeGrid, row: u32, current_tree: u32) -> bool {
    for col_offset in 0..col {
        let left_tree = grid_of_trees.trees[row as usize][col_offset as usize];
        if left_tree >= current_tree {
            return false;
        }
    }
    true
}

fn is_visible_from_right(col: u32, grid_of_trees: &TreeGrid, row: u32, current_tree: u32) -> bool {
    for col_offset in (col + 1)..grid_of_trees.columns {
        let right_tree = grid_of_trees.trees[row as usize][col_offset as usize];
        if right_tree >= current_tree {
            return false;
        }
    }
    true
}

fn parse_grid_of_trees(input: &str) -> TreeGrid {
    let lines = input.lines();
    let rows = lines.count() as u32;

    let lines = input.lines();
    let mut columns = 0;

    let trees: Vec<Vec<u32>> = lines
        .map(|line| {
            if columns == 0 {
                columns = line.len() as u32;
            }
            line.chars().map(|c| c.to_digit(10).unwrap()).collect()
        })
        .collect();
    TreeGrid {
        trees,
        rows,
        columns,
    }
}

pub fn process_part2(input: &str) -> u32 {
    let grid_of_trees = parse_grid_of_trees(input);
    let mut scenic_matrix: Vec<Vec<u32>> = Vec::new();

    for row in 0..grid_of_trees.rows {
        let mut scenic_line = Vec::new();

        for col in 0..grid_of_trees.columns {
            let current_tree = grid_of_trees.trees[row as usize][col as usize];

            let scenic_value = calculate_scenic_value(&grid_of_trees, row, col);

            scenic_line.push(scenic_value);
        }
        scenic_matrix.push(scenic_line);
    }

    let max_scenic_value = scenic_matrix.iter().flatten().map(|x| *x).max().unwrap();

    max_scenic_value
}

fn calculate_scenic_value(grid_of_trees: &TreeGrid, row: u32, col: u32) -> u32 {
    get_top_scenic_value(grid_of_trees, row, col)
        * get_bottom_scenic_value(grid_of_trees, row, col)
        * get_left_scenic_value(grid_of_trees, row, col)
        * get_right_scenic_value(grid_of_trees, row, col)
}

fn get_top_scenic_value(grid_of_trees: &TreeGrid, row: u32, col: u32) -> u32 {
    if row <= 0 {
        return 0;
    }

    let mut scenic_value = 0;
    let current_tree = grid_of_trees.trees[row as usize][col as usize];

    for row_offset in 1..=row {
        let target_row = row - row_offset;
        // println!("Iterating row {}...", target_row);
        let top_tree = grid_of_trees.trees[target_row as usize][col as usize];
        if row == target_row {
            // println!("Skipping because ({},{}) == ({},{})", row, col, target_row, col);
            continue;
        }
        // println!("Adding one point to tree {} at ({},{})", current_tree, row, col);
        scenic_value += 1;
        if top_tree >= current_tree {
            // println!("Breaking loop because tree {} at ({},{}) blocks view to {}", top_tree, target_row, col, current_tree);
            break;
        }
    }
    // println!("Top Scenic value for {}: {}", current_tree, scenic_value);
    scenic_value
}

fn get_bottom_scenic_value(grid_of_trees: &TreeGrid, row: u32, col: u32) -> u32 {
    if row >= grid_of_trees.rows - 1 {
        return 0;
    }

    let mut scenic_value = 0;
    let current_tree = grid_of_trees.trees[row as usize][col as usize];
    for target_row in (row + 1)..grid_of_trees.rows {
        let bottom_tree = grid_of_trees.trees[target_row as usize][col as usize];
        if row == target_row {
            continue;
        }
        scenic_value += 1;
        if bottom_tree >= current_tree {
            break;
        }
    }
    scenic_value
}

fn get_left_scenic_value(grid_of_trees: &TreeGrid, row: u32, col: u32) -> u32 {
    if col <= 0 {
        return 0;
    }

    let mut scenic_value = 0;
    let current_tree = grid_of_trees.trees[row as usize][col as usize];

    for col_offset in 1..=col {
        let target_col = col - col_offset;
        let left_tree = grid_of_trees.trees[row as usize][target_col as usize];
        if col == target_col {
            continue;
        }
        scenic_value += 1;
        if left_tree >= current_tree {
            break;
        }
    }
    scenic_value
}

fn get_right_scenic_value(grid_of_trees: &TreeGrid, row: u32, col: u32) -> u32 {
    if col >= grid_of_trees.columns - 1 {
        return 0;
    }

    let mut scenic_value = 0;
    let current_tree = grid_of_trees.trees[row as usize][col as usize];
    for target_col in (col + 1)..grid_of_trees.columns {
        let right_tree = grid_of_trees.trees[row as usize][target_col as usize];
        if col == target_col {
            continue;
        }
        scenic_value += 1;
        if right_tree >= current_tree {
            break;
        }
    }
    scenic_value
}

struct TreeGrid {
    trees: Vec<Vec<u32>>,
    rows: u32,
    columns: u32,
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "30373
25512
65332
33549
35390";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, 21);
    }

    #[test]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 8);
    }
}
