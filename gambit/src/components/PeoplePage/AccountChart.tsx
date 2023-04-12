import Card from "@mui/material/Card";
import AccountInOut from "../../models/Display/AccountInOut";
import CardContent from "@mui/material/CardContent";
import ReactEChartsCore from 'echarts-for-react/lib/core';
import * as echarts from 'echarts/core';
import { PieChart, PieSeriesOption } from 'echarts/charts';
import {
  TooltipComponent,
  TooltipComponentOption,
  LegendComponent,
  LegendComponentOption
} from 'echarts/components';
import { LabelLayout } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';

echarts.use([
  TooltipComponent,
  LegendComponent,
  PieChart,
  CanvasRenderer,
  LabelLayout
]);
type EChartsOption = echarts.ComposeOption<
  TooltipComponentOption | LegendComponentOption | PieSeriesOption
>;



class Contable {
  name: string;
  value: number;

  constructor(name: string, value: number)
  {
    this.name = name;
    this.value = value;
  }
}

const AccountChart = ({ accounts } : { accounts: Array<AccountInOut>}) => {
  const Plus = accounts.map(e => new Contable(e.name, e.entry));
  const PlusValue = Plus.reduce((acc, item) => acc + item.value, 0);
  const Exits = accounts.map(e => new Contable(e.name, e.exit));
  const ExitsValue = Exits.reduce((acc, item) => acc + item.value, 0);

  const ChartOptions = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      top: '5%',
      left: 'center',
      // doesn't perfectly work with our tricks, disable it
      selectedMode: true
    },
    series: [
      {
        name: 'Access From',
        type: 'pie',
        radius: ['40%', '70%'],
        center: ['50%', '70%'],
        // adjust the start angle
        startAngle: 180,
        label: {
          show: true,
          formatter(param: { name: string; percent: any; }) {
            // correct the percentage
            return param.name + ' (' + param.percent! * 2 + '%)';
          }
        },
        data: [
          { value: Plus, name: 'Income', itemStyle: { color: '#000'} },
          { value: Exits, name: 'Exits', itemStyle: { color: '#c0272c'}},
          {
            // make an record to fill the bottom 50%
            value: PlusValue + ExitsValue,
            itemStyle: {
              // stop the chart from rendering this piece
              color: 'none',
              decal: {
                symbol: 'none'
              }
            },
            label: {
              show: false
            }
          }
        ]
      }
    ]
  }

  return(
    <Card>
      <CardContent>
        <ReactEChartsCore
          echarts={echarts}
          option={ChartOptions}
          notMerge={true}
          lazyUpdate={true}
        />
      </CardContent>
    </Card>
  );
}

export default AccountChart;