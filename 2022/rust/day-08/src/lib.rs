pub fn process_part1(input: &str) -> u32 {
    let grid_of_trees = parse_grid_of_trees(input);
    count_visible_trees(grid_of_trees)
}

fn count_visible_trees(grid_of_trees: TreeGrid) -> u32 {
    let edge_visible_trees = 2*(grid_of_trees.rows - 2 + grid_of_trees.columns);
    let mut interior_visible_trees = 0;

    for row in 1..grid_of_trees.rows - 1 {
        for col in 1..grid_of_trees.columns - 1 {
            // println!("____________________");
            let current_tree = grid_of_trees.trees[row as usize][col as usize];

            if
            is_visible_from_above(row, &grid_of_trees, col, current_tree) 
            || is_visible_from_left(col, &grid_of_trees, row, current_tree)
            || is_visible_from_below(row, &grid_of_trees, col, current_tree)
            || is_visible_from_right(col, &grid_of_trees, row, current_tree) 
            {
                interior_visible_trees += 1;
                // println!("Tree at {},{} {} is visible", row, col, current_tree);
            }
        }
    }
    println!("Edge: {}, Interior: {}", edge_visible_trees,  interior_visible_trees);
    edge_visible_trees + interior_visible_trees
}

fn is_visible_from_above(row: u32, grid_of_trees: &TreeGrid, col: u32, current_tree: u32) -> bool {
    // check up
    for row_offset in 0..row {
        let top_tree = grid_of_trees.trees[row_offset as usize][col as usize];
        // println!("Comparing {} to {} from above ({},{}) and ({},{})", top_tree, current_tree, row_offset, col, row, col);
        if top_tree >= current_tree {
            return false;
        }
    }
    // println!("Visible from Above");
    true
}

fn is_visible_from_below(row: u32, grid_of_trees: &TreeGrid, col: u32, current_tree: u32) -> bool{
    // check down
    for row_offset in (row + 1)..grid_of_trees.rows {
        let bottom_tree = grid_of_trees.trees[row_offset as usize][col as usize];
        // println!("Comparing {} to {} from above ({},{}) and ({},{})", bottom_tree, current_tree, row_offset, col, row, col);
        if bottom_tree >= current_tree {
            return false;
        }
    }
    // println!("Visible from Below");
    true
}

fn is_visible_from_left(col: u32, grid_of_trees: &TreeGrid, row: u32, current_tree: u32) -> bool {
    // check left
    for col_offset in 0..col {
        let left_tree = grid_of_trees.trees[row as usize][col_offset as usize];
        // println!("Comparing {} to {} from above ({},{}) and ({},{})", left_tree, current_tree, row, col_offset, row, col);
        if left_tree >= current_tree {
            return false;
        }
    }
    // println!("Visible from Left");
    true
}

fn is_visible_from_right(col: u32, grid_of_trees: &TreeGrid, row: u32, current_tree: u32) -> bool {
    // check right
    for col_offset in (col + 1)..grid_of_trees.columns {
        let right_tree = grid_of_trees.trees[row as usize][col_offset as usize];
        // println!("Comparing {} to {} from above ({},{}) and ({},{})", right_tree, current_tree, row, col_offset, row, col);
        if right_tree >= current_tree {
            return false;
        }
    }
    // println!("Visible from Right");
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
    0
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
    #[ignore]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 45000);
    }
}
