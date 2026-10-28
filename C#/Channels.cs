
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("========== C# Channels Demo ==========");

        using var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        // Bounded channel with a capacity of 5 buffered items.
        var breadCounterChannel =
            Channel.CreateBounded<string>(
                new BoundedChannelOptions(5)
                {
                    FullMode = BoundedChannelFullMode.Wait,
                    SingleWriter = true,
                    SingleReader = false
                });

        // Start the producer.
        Task chefTask = PrepareBread(
            breadCounterChannel.Writer,
            cts.Token);

        // Start two consumers reading from the same channel.
        Task customer1Task = PickUpBread(
            breadCounterChannel.Reader,
            "Customer 1",
            cts.Token);

        Task customer2Task = PickUpBread(
            breadCounterChannel.Reader,
            "Customer 2",
            cts.Token);

        try
        {
            await Task.WhenAll(
                chefTask,
                customer1Task,
                customer2Task);

            Console.WriteLine(
                "All breads have been processed successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine(
                "Channel processing was cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Processing failed: {ex.Message}");
        }

        Console.WriteLine("Application finished.");
    }

    // Producer: prepares bread and writes it to the channel.
    static async Task PrepareBread(
        ChannelWriter<string> writer,
        CancellationToken cancellationToken)
    {
        Exception? failure = null;

        try
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string bread = $"Bread-{i}";

                await writer.WriteAsync(
                    bread,
                    cancellationToken);

                Console.WriteLine(
                    $"Chef prepared: {bread}");

                await Task.Delay(
                    200,
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            failure = ex;
            throw;
        }
        finally
        {
            // Signal that no more items will be written.
            // Propagate a producer failure to the channel reader.
            writer.TryComplete(failure);
        }
    }

    // Consumer: reads and processes available bread.
    static async Task PickUpBread(
        ChannelReader<string> reader,
        string customerName,
        CancellationToken cancellationToken)
    {
        await foreach (
            string bread in reader.ReadAllAsync(cancellationToken))
        {
            Console.WriteLine(
                $"{customerName} picked up: {bread}");

            // Simulate the time required to serve a customer.
            await Task.Delay(
                500,
                cancellationToken);
        }

        Console.WriteLine($"{customerName} finished.");
    }
}
