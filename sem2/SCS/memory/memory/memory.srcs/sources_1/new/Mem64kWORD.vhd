----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 14.05.2018 09:29:46
-- Design Name: 
-- Module Name: Mem64kWORD - Behavioral
-- Project Name: 
-- Target Devices: 
-- Tool Versions: 
-- Description: 
-- 
-- Dependencies: 
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
-- 
----------------------------------------------------------------------------------


library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx leaf cells in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity Mem64kWORD is
    Port (data : inout std_logic_vector(15 downto 0); address : in std_logic_vector(16 downto 0); nSEL : in std_logic; nWR : in std_logic; nBHE : in std_logic );
end Mem64kWORD;

architecture Behavioral of Mem64kWORD is

component Mem64k8b is
    Port (data : inout std_logic_vector(7 downto 0); address : in std_logic_vector(15 downto 0); nCS : in std_logic; nWR : in std_logic );
end component;

signal selLow, selHigh:std_logic;

begin

LB: Mem64k8b port map (data(7 downto 0), address(16 downto 1), selLow, nWR);
HB: Mem64k8b port map (data(15 downto 8), address(16 downto 1), selHigh, nWR);

selLow <= nSEL or address(0);
selHigh <= nSEL or nBHE;

end Behavioral;
