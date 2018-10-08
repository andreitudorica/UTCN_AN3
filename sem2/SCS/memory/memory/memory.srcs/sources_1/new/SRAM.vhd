----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 14.05.2018 09:39:37
-- Design Name: 
-- Module Name: SRAM - Behavioral
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

entity SRAM is
    Port (data : inout std_logic_vector(15 downto 0); address : in std_logic_vector(23 downto 0); nWR : in std_logic; nRD : in std_logic; nBHE : in std_logic );
end SRAM;

architecture Behavioral of SRAM is

component Mem64kWORD is
    Port (data : inout std_logic_vector(15 downto 0); address : in std_logic_vector(16 downto 0); nSEL : in std_logic; nWR : in std_logic; nBHE : in std_logic );
end component;

signal sel : std_logic_vector(0 to 7);

begin

MB0: Mem64kWORD port map (data, address(16 downto 0), sel(0), nWR, nBHE);
MB1: Mem64kWORD port map (data, address(16 downto 0), sel(1), nWR, nBHE);
MB2: Mem64kWORD port map (data, address(16 downto 0), sel(2), nWR, nBHE);
MB3: Mem64kWORD port map (data, address(16 downto 0), sel(3), nWR, nBHE);
MB4: Mem64kWORD port map (data, address(16 downto 0), sel(4), nWR, nBHE);
MB5: Mem64kWORD port map (data, address(16 downto 0), sel(5), nWR, nBHE);
MB6: Mem64kWORD port map (data, address(16 downto 0), sel(6), nWR, nBHE);
MB7: Mem64kWORD port map (data, address(16 downto 0), sel(7), nWR, nBHE);

process(address, nWR, nRD)
begin
    if nRD = '0' or nWR = '0' then
        if address(23 downto 20) = "1100" then
            case address(19 downto 17) is
                when "000" => sel <= "01111111";
                when "001" => sel <= "10111111";
                when "010" => sel <= "11011111";
                when "011" => sel <= "11101111";
                when "100" => sel <= "11110111";
                when "101" => sel <= "11111011";
                when "110" => sel <= "11111101";
                when "111" => sel <= "11111110";
            end case;
        end if;
    end if;
end process;


end Behavioral;
