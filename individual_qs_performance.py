# -*- coding: utf-8 -*-
"""Individual Qs Performance

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/16GcIESeWO2_rkH2ezf4dE8We9E8pCvbI
"""

import pandas as pd
from collections import defaultdict, Counter
from google.colab import files
import os
import csv  # Import the csv module

#finds all the values that repeat

# Upload multiple CSV files
# uploaded = files.upload()

# # Process each uploaded file
# for file_name in uploaded.keys():
#     print(f"Processing file: {file_name}")

#     # Read the uploaded CSV file into a DataFrame
#     df = pd.read_csv(file_name)

#     # Count occurrences of values in the first column
#     value_counts = Counter(df[df.columns[0]])

#     # Filter out values that appear only once
#     duplicates = {key for key, count in value_counts.items() if count > 1}

#     # Filter rows to include only those with duplicated values in the first column
#     duplicates_df = df[df[df.columns[0]].isin(duplicates)]

#     # If there are no duplicated values, print a message and continue to the next file
#     if len(duplicates_df) == 0:
#         print("No repeated values found in the first column. No export performed.")
#         continue

#     # Initialize a dictionary to store rows grouped by their values in the first column
#     clustered_rows = defaultdict(list)

#     # Group rows by their values in the first column
#     for index, row in duplicates_df.iterrows():
#         clustered_rows[row[df.columns[0]]].append(row.values.tolist())  # Append row as a list

#     # Concatenate 'repeats' with the original file name for the exported file name
#     export_file_name = f"{os.path.splitext(file_name)[0]}_repeats.csv"

#     # Export grouped DataFrame to CSV with the modified file name
#     with open(export_file_name, 'w', newline='') as file:
#         writer = csv.writer(file)
#         for key, rows in clustered_rows.items():
#             for row in rows:
#                 writer.writerow(row)

#     # Initiate download of the exported CSV file
#     files.download(export_file_name)

import matplotlib.pyplot as plt

uploaded = files.upload()

for file_name in uploaded.keys():
  print(f"Processing file: {file_name}")

  # Read the uploaded CSV file into a DataFrame
  df = pd.read_csv(file_name)

  # Initialize variables for plotting
  x_values = [0]
  y_values = [0]
  current_value = None

  # Iterate through the DataFrame to extract x and y values
  for index, row in df.iterrows():
      if row[0] != current_value:
          if current_value is not None:
              plt.plot(x_values, y_values, label=f'{current_value}')
          current_value = row[0]
          x_values = [0]  # Start x-values from 0
          y_values = [0]  # Start y-values from 0
          y_accumulator = 0  # Reset y accumulator for each new line
      x_values.append(len(x_values))  # Index starts from 0, so we add 1 for x-values
      if row[1] == 0:
        y_accumulator -= 1;
      y_accumulator += row[1]  # Accumulate changes

      y_values.append(y_accumulator)



  # Plot the last set of values
  plt.plot(x_values, y_values, label=f'{current_value}')

  # Add labels and legend
  plt.xlabel('Occurrence Number')
  plt.ylabel('Accumulated Second Column Value')
  plt.title('Line Chart for Each Unique Value Starting from (0,0)')
  plt.legend(title='First Column')

  # Show the plot
  plt.show()